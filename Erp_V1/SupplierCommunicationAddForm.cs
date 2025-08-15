// File: SupplierCommunicationAddForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SupplierCommunicationAddForm : MaterialForm
    {
        private readonly SupplierCommunicationBLL _commBll = new SupplierCommunicationBLL();
        private readonly SupplierBLL _supBll = new SupplierBLL();
        private readonly bool _isEdit;
        private readonly SupplierCommunicationDetailDTO _dto;

        public SupplierCommunicationAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEdit = false;
            Text = "Add Supplier Communication";
        }

        public SupplierCommunicationAddForm(SupplierCommunicationDetailDTO dto)
            : this()
        {
            _isEdit = true;
            _dto = dto ?? throw new ArgumentNullException(nameof(dto));
            Text = "Edit Supplier Communication";
        }

        private async void SupplierCommunicationAddForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Fetch all active suppliers
                var supDto = await Task.Run(() => _supBll.Select());

                // ❗ CRITICAL FIX: Use BindingSource for MaterialSkin compatibility
                var bindingSource = new BindingSource();

                if (supDto.Suppliers == null || !supDto.Suppliers.Any())
                {
                    MessageBox.Show("No active suppliers found. Please add a supplier first.",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSupplier.Enabled = false;
                    return;
                }

                // Configure BindingSource
                bindingSource.DataSource = supDto.Suppliers;

                // Configure ComboBox
                cmbSupplier.DataSource = bindingSource;
                cmbSupplier.DisplayMember = nameof(SupplierDetailDTO.SupplierName);
                cmbSupplier.ValueMember = nameof(SupplierDetailDTO.SupplierID);
                cmbSupplier.SelectedIndex = -1; // Ensure no initial selection

                // Rest of your logic...
                if (_isEdit)
                {
                    cmbSupplier.SelectedValue = _dto.SupplierID;
                    // ... other edit logic
                }
                else
                {
                    dtpDate.Value = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading form data:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void InitializeMaterialSkin()
        {
            var mgr = MaterialSkinManager.Instance;
            mgr.AddFormToManage(this);
            mgr.Theme = MaterialSkinManager.Themes.LIGHT;
            mgr.ColorScheme = new ColorScheme(
                Primary.Green500, Primary.Green700,
                Primary.Green100, Accent.Lime200,
                TextShade.WHITE
            );
        }

        #region Validation

        private void cmbSupplier_Validating(object sender, CancelEventArgs e)
        {
            if (cmbSupplier.SelectedItem == null)
            {
                e.Cancel = true;
                cmbSupplier.Focus();
                errorProvider.SetError(cmbSupplier, "Supplier selection is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(cmbSupplier, null);
            }
        }

        private void txtType_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                e.Cancel = true;
                txtType.Focus();
                errorProvider.SetError(txtType, "Communication type is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtType, null);
            }
        }

        private void txtSubject_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                e.Cancel = true;
                txtSubject.Focus();
                errorProvider.SetError(txtSubject, "Subject is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtSubject, null);
            }
        }

        #endregion

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show("Please correct validation errors before saving.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = _isEdit ? _dto : new SupplierCommunicationDetailDTO();
            dto.SupplierID = (int)cmbSupplier.SelectedValue;
            dto.CommunicationDate = dtpDate.Value.Date;
            dto.Type = txtType.Text.Trim();
            dto.Subject = txtSubject.Text.Trim();
            dto.Content = txtContent.Text.Trim();
            dto.ReferenceLink = txtReference.Text.Trim();

            try
            {
                var success = await Task.Run(() =>
                    _isEdit
                        ? _commBll.Update(dto)
                        : _commBll.Insert(dto)
                );

                if (success)
                {
                    MessageBox.Show("Operation completed successfully.", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Save operation failed. Please check the data and try again.", "Operation Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder($"An unexpected error occurred: {ex.Message}");
                for (var inner = ex.InnerException; inner != null; inner = inner.InnerException)
                {
                    sb.AppendLine($"\nInner Exception: {inner.Message}");
                }

                MessageBox.Show(sb.ToString(), "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       
    }
}