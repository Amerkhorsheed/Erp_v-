// File: CustomerContractAddForm.cs
using System;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Erp_V1
{
    public partial class CustomerContractAddForm : MaterialForm
    {
        private readonly CustomerContractBLL _contractBll = new CustomerContractBLL();
        private readonly CustomerBLL _customerBll = new CustomerBLL();
        private CustomerContractDTO _dto;
        private readonly bool _isEditMode;

        public CustomerContractAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            Load += CustomerContractAddForm_Load;
            _isEditMode = false;
            Text = "Add Customer Contract";
        }

        public CustomerContractAddForm(CustomerContractDTO dtoToEdit)
            : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            Text = "Edit Customer Contract";
        }

        private void InitializeMaterialSkin()
        {
            var manager = MaterialSkinManager.Instance;
            manager.AddFormToManage(this);
            manager.Theme = MaterialSkinManager.Themes.LIGHT;
            manager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800,
                Primary.BlueGrey900,
                Primary.BlueGrey500,
                Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private async void CustomerContractAddForm_Load(object sender, EventArgs e)
        {
            var customers = await _customerBll.SelectAsync();
            customerComboBox.DisplayMember = nameof(CustomerDetailDTO.CustomerName);
            customerComboBox.ValueMember = nameof(CustomerDetailDTO.ID);
            customerComboBox.DataSource = customers;

            statusComboBox.DataSource = Enum.GetValues(typeof(ContractStatus));

            hasEndDateCheckBox.CheckedChanged += (_, __) =>
                endDatePicker.Enabled = hasEndDateCheckBox.Checked;

            if (_isEditMode)
                PopulateFields();
        }

        private void PopulateFields()
        {
            customerComboBox.SelectedValue = _dto.CustomerID;
            contractNumberTextBox.Text = _dto.ContractNumber;
            descriptionTextBox.Text = _dto.Description;
            startDatePicker.Value = _dto.StartDate;
            hasEndDateCheckBox.Checked = _dto.EndDate.HasValue;
            endDatePicker.Value = _dto.EndDate ?? DateTime.Today;
            statusComboBox.SelectedItem = Enum.Parse(typeof(ContractStatus), _dto.Status);
        }

        private void CustomerContractAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                saveButton.PerformClick();
            }
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show(
                    "Please fix validation errors first.",
                    "Validation Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

            var dto = _isEditMode ? _dto : new CustomerContractDTO { IsDeleted = false };
            dto.CustomerID = (int)customerComboBox.SelectedValue;
            dto.ContractNumber = contractNumberTextBox.Text.Trim();
            dto.Description = descriptionTextBox.Text.Trim();
            dto.StartDate = startDatePicker.Value.Date;
            dto.EndDate = hasEndDateCheckBox.Checked
                                   ? (DateTime?)endDatePicker.Value.Date
                                   : null;
            dto.Status = statusComboBox.SelectedItem.ToString();

            saveButton.Enabled = false;
            try
            {
                bool success = _isEditMode
                    ? await _contractBll.UpdateAsync(dto)
                    : await _contractBll.InsertAsync(dto);

                if (success)
                    DialogResult = DialogResult.OK;
                else
                    MessageBox.Show("No changes were saved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Save failed:\n\n{GetExceptionDetails(ex)}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
            finally
            {
                saveButton.Enabled = true;
            }
        }

        private string GetExceptionDetails(Exception ex, int level = 0)
        {
            if (ex == null) return "";
            var indent = new string('>', level * 2);
            var msg = $"{indent}{ex.GetType().Name}: {ex.Message}\n";
            if (ex is DbEntityValidationException vex)
            {
                foreach (var err in vex.EntityValidationErrors.SelectMany(v => v.ValidationErrors))
                    msg += $"{indent}  - {err.PropertyName}: {err.ErrorMessage}\n";
            }
            return msg + GetExceptionDetails(ex.InnerException, level + 1);
        }

        #region Validation

        private void customerComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (customerComboBox.SelectedIndex < 0)
            {
                e.Cancel = true;
                errorProvider.SetError(customerComboBox, "Please select a customer.");
            }
            else
                errorProvider.SetError(customerComboBox, "");
        }

        private void contractNumberTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contractNumberTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(contractNumberTextBox, "Contract number is required.");
            }
            else
                errorProvider.SetError(contractNumberTextBox, "");
        }

        private void statusComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (statusComboBox.SelectedIndex < 0)
            {
                e.Cancel = true;
                errorProvider.SetError(statusComboBox, "Please select a status.");
            }
            else
                errorProvider.SetError(statusComboBox, "");
        }

        #endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
