// File: CustomerPointsAddForm.cs
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class CustomerPointsAddForm : Form
    {
        private readonly CustomerPointsBLL _bll = new CustomerPointsBLL();
        private CustomerPointsDTO _dto;
        private readonly bool _isEditMode;

        public CustomerPointsAddForm()
        {
            InitializeComponent();
            Load += CustomerPointsAddForm_Load;
            _isEditMode = false;
            Text = "Add Customer Points";
        }

        public CustomerPointsAddForm(CustomerPointsDTO dtoToEdit)
            : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            Text = "Edit Customer Points";
        }

        private async void CustomerPointsAddForm_Load(object sender, EventArgs e)
        {
            // load customers
            var customers = await new CustomerBLL().SelectAsync();
            customerComboBox.DisplayMember = nameof(CustomerDetailDTO.CustomerName);
            customerComboBox.ValueMember = nameof(CustomerDetailDTO.ID);
            customerComboBox.DataSource = customers;

            if (_isEditMode)
            {
                customerComboBox.SelectedValue = _dto.CustomerID;
                pointsNumericUpDown.Value = _dto.Points;
            }
        }

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

        private void pointsNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if (pointsNumericUpDown.Value <= 0)
            {
                e.Cancel = true;
                errorProvider.SetError(pointsNumericUpDown, "Points must be greater than zero.");
            }
            else
                errorProvider.SetError(pointsNumericUpDown, "");
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fix validation errors first.",
                                "Validation Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _isEditMode
                ? _dto
                : new CustomerPointsDTO();

            dto.CustomerID = (int)customerComboBox.SelectedValue;
            dto.Points = (long)pointsNumericUpDown.Value;

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
                MessageBox.Show($"Save failed:\n\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveButton.Enabled = true;
                return;
            }

            saveButton.Enabled = true;
            DialogResult = success ? DialogResult.OK : DialogResult.None;
        }

        private void cancelButton_Click(object sender, EventArgs e)
            => DialogResult = DialogResult.Cancel;
    }
}
