// File: CustomerInteractionAddForm.cs
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class CustomerInteractionAddForm : Form
    {
        private readonly CustomerInteractionBLL _bll = new CustomerInteractionBLL();
        private CustomerInteractionDTO _dto;
        private readonly bool _isEditMode;

        public CustomerInteractionAddForm()
        {
            InitializeComponent();
            Load += CustomerInteractionAddForm_Load;
            _isEditMode = false;
            Text = "Add Customer Interaction";
        }

        public CustomerInteractionAddForm(CustomerInteractionDTO dtoToEdit)
            : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            Text = "Edit Customer Interaction";
        }

        private async void CustomerInteractionAddForm_Load(object sender, EventArgs e)
        {
            // load customers
            var customers = await new CustomerBLL().SelectAsync();
            customerComboBox.DisplayMember = nameof(CustomerDetailDTO.CustomerName);
            customerComboBox.ValueMember = nameof(CustomerDetailDTO.ID);
            customerComboBox.DataSource = customers;

            if (_isEditMode)
                PopulateFields();
        }

        private void PopulateFields()
        {
            customerComboBox.SelectedValue = _dto.CustomerID;
            typeTextBox.Text = _dto.Type;
            notesTextBox.Text = _dto.Notes;
            interactionDatePicker.Value = _dto.InteractionDate;
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

        private void typeTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(typeTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(typeTextBox, "Type is required.");
            }
            else
                errorProvider.SetError(typeTextBox, "");
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
                : new CustomerInteractionDTO { /* InteractionDate set below */ };

            dto.CustomerID = (int)customerComboBox.SelectedValue;
            dto.Type = typeTextBox.Text.Trim();
            dto.Notes = notesTextBox.Text.Trim();
            dto.InteractionDate = interactionDatePicker.Value.Date;

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
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
