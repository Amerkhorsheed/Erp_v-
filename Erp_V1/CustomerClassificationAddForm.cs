// File: CustomerClassificationAddForm.cs
using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Erp_V1
{
    public partial class CustomerClassificationAddForm : MaterialForm
    {
        private readonly CustomerClassificationBLL _bll = new CustomerClassificationBLL();
        private CustomerClassificationDTO _dto;
        private readonly bool _isEditMode;

        public CustomerClassificationAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEditMode = false;
            this.KeyPreview = true;
            this.KeyDown += CustomerClassificationAddForm_KeyDown;
            Text = "Add Customer Classification";
        }

        public CustomerClassificationAddForm(CustomerClassificationDTO dtoToEdit)
            : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            Text = "Edit Customer Classification";
        }

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.DeepPurple500,
                Primary.DeepPurple700,
                Primary.DeepPurple100,
                Accent.Pink200,
                TextShade.WHITE
            );
        }

        private async void CustomerClassificationAddForm_Load(object sender, EventArgs e)
        {
            var customers = await new CustomerBLL().SelectAsync();
            customerComboBox.DisplayMember = nameof(CustomerDetailDTO.CustomerName);
            customerComboBox.ValueMember = nameof(CustomerDetailDTO.ID);
            customerComboBox.DataSource = customers;

            if (_isEditMode)
            {
                customerComboBox.SelectedValue = _dto.CustomerID;
                tierTextBox.Text = _dto.Tier;
                assignedDatePicker.Value = _dto.AssignedDate;
            }
            else
            {
                assignedDatePicker.Value = DateTime.Today;
            }
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
            {
                errorProvider.SetError(customerComboBox, "");
            }
        }

        private void tierTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tierTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(tierTextBox, "Tier is required.");
            }
            else
            {
                errorProvider.SetError(tierTextBox, "");
            }
        }

        #endregion

        private void CustomerClassificationAddForm_KeyDown(object sender, KeyEventArgs e)
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
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var dto = _isEditMode ? _dto : new CustomerClassificationDTO();
            dto.CustomerID = (int)customerComboBox.SelectedValue;
            dto.Tier = tierTextBox.Text.Trim();
            dto.AssignedDate = assignedDatePicker.Value.Date;

            saveButton.Enabled = false;
            bool success;
            try
            {
                success = _isEditMode
                    ? await _bll.UpdateAsync(dto)
                    : await _bll.InsertAsync(dto);
            }
            catch (Exception ex)
            {
                var eb = new StringBuilder($"Save failed: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    eb.AppendLine($"\nInner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }
                MessageBox.Show(eb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveButton.Enabled = true;
                return;
            }

            saveButton.Enabled = true;
            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Operation did not succeed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
