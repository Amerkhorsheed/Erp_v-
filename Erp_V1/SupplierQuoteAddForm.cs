// File: SupplierQuoteAddForm.cs
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class SupplierQuoteAddForm : MaterialForm
    {
        private readonly SupplierQuoteBLL _supplierQuoteBll = new SupplierQuoteBLL();
        private readonly SupplierBLL _supplierBll = new SupplierBLL();
        private SupplierQuoteDetailDTO _currentQuoteDto;
        private readonly bool _isEditMode;

        public SupplierQuoteAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();

            _isEditMode = false;
            Text = "Add New Supplier Quote";
        }

        public SupplierQuoteAddForm(SupplierQuoteDetailDTO dto) : this()
        {
            _currentQuoteDto = dto ?? throw new ArgumentNullException(nameof(dto), "SupplierQuoteDetailDTO cannot be null in edit mode.");

            _isEditMode = true;
            Text = "Edit Supplier Quote";
        }

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Amber500, Primary.Amber700,
                Primary.Amber100, Accent.Orange200,
                TextShade.WHITE
            );
        }

        private void SupplierQuoteAddForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            PopulateFormData();
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = _supplierBll.Select().Suppliers;
                comboSupplier.DataSource = suppliers;
                comboSupplier.DisplayMember = nameof(SupplierDetailDTO.SupplierName);
                comboSupplier.ValueMember = nameof(SupplierDetailDTO.SupplierID);
                comboSupplier.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to load suppliers.", ex);
            }
        }

        private void PopulateFormData()
        {
            if (_isEditMode && _currentQuoteDto != null)
            {
                comboSupplier.SelectedValue = _currentQuoteDto.SupplierID;
                dtpDate.Value = _currentQuoteDto.QuoteDate;
                numAmount.Value = _currentQuoteDto.TotalAmount;
                txtCurrency.Text = _currentQuoteDto.Currency;
                txtDetails.Text = _currentQuoteDto.Details;
            }
            else
            {
                dtpDate.Value = DateTime.Today;
                numAmount.Value = 0m;
                txtCurrency.Text = string.Empty;
                txtDetails.Text = string.Empty;
            }
        }

        #region Validation Event Handlers

        private void comboSupplier_Validating(object sender, CancelEventArgs e)
        {
            if (comboSupplier.SelectedIndex < 0)
            {
                e.Cancel = true;
                errorProvider.SetError(comboSupplier, "Please select a supplier.");
            }
            else
            {
                errorProvider.SetError(comboSupplier, string.Empty);
            }
        }

        private void txtCurrency_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrency.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtCurrency, "Currency is required.");
            }
            else
            {
                errorProvider.SetError(txtCurrency, string.Empty);
            }
        }

        private void numAmount_Validating(object sender, CancelEventArgs e)
        {
            if (numAmount.Value <= 0)
            {
                e.Cancel = true;
                errorProvider.SetError(numAmount, "Amount must be positive.");
            }
            else
            {
                errorProvider.SetError(numAmount, string.Empty);
            }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            // The 'ValidationGroup' parameter is not recognized by default ValidateChildren().
            // Simply call ValidateChildren() without arguments to validate all enabled controls.
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fix validation errors before saving.", "Validation Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var quoteToSave = _isEditMode ? _currentQuoteDto : new SupplierQuoteDetailDTO();

            quoteToSave.SupplierID = (int)comboSupplier.SelectedValue;
            quoteToSave.QuoteDate = dtpDate.Value.Date;
            quoteToSave.TotalAmount = numAmount.Value;
            quoteToSave.Currency = txtCurrency.Text.Trim();
            quoteToSave.Details = txtDetails.Text.Trim();

            try
            {
                bool success;
                string successMessage;

                if (_isEditMode)
                {
                    success = _supplierQuoteBll.Update(quoteToSave);
                    successMessage = "Supplier quote updated successfully!";
                }
                else
                {
                    success = _supplierQuoteBll.Insert(quoteToSave);
                    successMessage = "New supplier quote added successfully!";
                }

                if (success)
                {
                    MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Operation did not succeed. Please try again.", "Operation Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while saving the supplier quote.", ex);
            }
        }

        private void ShowErrorMessage(string message, Exception exception = null)
        {
            var sb = new StringBuilder(message);
            if (exception != null)
            {
                sb.AppendLine($"\n\nDetails: {exception.Message}");
                for (var inner = exception.InnerException; inner != null; inner = inner.InnerException)
                {
                    sb.AppendLine($"Inner Exception: {inner.Message}");
                }
            }

            MessageBox.Show(sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
