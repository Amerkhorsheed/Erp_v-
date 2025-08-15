// File: CustomerDocumentAddForm.cs
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class CustomerDocumentAddForm : Form
    {
        private readonly CustomerDocumentBLL _bll = new CustomerDocumentBLL();
        private CustomerDocumentDTO _dto;
        private readonly bool _isEditMode;

        public CustomerDocumentAddForm()
        {
            InitializeComponent();
            Load += CustomerDocumentAddForm_Load;

            _isEditMode = false;
            Text = "Add Customer Document";
        }

        public CustomerDocumentAddForm(CustomerDocumentDTO dtoToEdit)
            : this()
        {
            _isEditMode = true;
            _dto = dtoToEdit;
            Text = "Edit Customer Document";
        }

        private async void CustomerDocumentAddForm_Load(object sender, EventArgs e)
        {
            // 1) Load customers into combo
            var customers = await new CustomerBLL().SelectAsync();
            customerComboBox.DisplayMember = nameof(CustomerDetailDTO.CustomerName);
            customerComboBox.ValueMember = nameof(CustomerDetailDTO.ID);
            customerComboBox.DataSource = customers;

            // 2) If edit mode, populate fields
            if (_isEditMode)
                PopulateFields();
        }

        private void PopulateFields()
        {
            customerComboBox.SelectedValue = _dto.CustomerID;
            filePathTextBox.Text = _dto.FilePath;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "All files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                    filePathTextBox.Text = dlg.FileName;
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
            {
                errorProvider.SetError(customerComboBox, "");
            }
        }

        private void filePathTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filePathTextBox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(filePathTextBox, "Please select a document file.");
            }
            else
            {
                errorProvider.SetError(filePathTextBox, "");
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

            var dto = _isEditMode
                ? _dto
                : new CustomerDocumentDTO { /* UploadDate will be set in BLL */ };

            dto.CustomerID = (int)customerComboBox.SelectedValue;
            dto.FilePath = filePathTextBox.Text;
            dto.FileName = Path.GetFileName(dto.FilePath);

            saveButton.Enabled = false;
            try
            {
                bool success = _isEditMode
                    ? await _bll.UpdateAsync(dto)
                    : await _bll.InsertAsync(dto);

                if (success)
                    DialogResult = DialogResult.OK;
                else
                    MessageBox.Show(
                        "No changes were saved.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Save failed:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                saveButton.Enabled = true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
