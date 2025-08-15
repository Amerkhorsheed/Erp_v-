using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Linq; // Added for LINQ
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SupplierContractAddForm : MaterialForm
    {
        private readonly SupplierContractBLL _bll = new SupplierContractBLL();
        private readonly SupplierBLL _supplierBll = new SupplierBLL(); // New: BLL for suppliers
        private readonly bool _isEditMode;
        private SupplierContractDetailDTO _currentDto;


        public SupplierContractAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEditMode = false;
            Text = "Add New Supplier Contract";
        }

        public SupplierContractAddForm(SupplierContractDetailDTO dto) : this()
        {
            _isEditMode = true;
            _currentDto = dto;
            Text = "Edit Supplier Contract";
        }

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Teal500, Primary.Teal700,
                Primary.Teal100, Accent.Cyan200,
                TextShade.WHITE
            );
        }

        private void SupplierContractAddForm_Load(object sender, EventArgs e)
        {
            // Load suppliers into the ComboBox
            try
            {
                var supplierDto = _supplierBll.Select();
                if (supplierDto != null && supplierDto.Suppliers != null)
                {
                    cbSupplier.DataSource = supplierDto.Suppliers;
                    cbSupplier.DisplayMember = "SupplierName"; // Display the name
                    cbSupplier.ValueMember = "SupplierID";     // Use the ID as the value
                    cbSupplier.SelectedIndex = -1; // No item selected by default
                }
                else
                {
                    MaterialMessageBox.Show("No suppliers found. Please add suppliers first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = false; // Disable save if no suppliers
                }
            }
            catch (Exception ex)
            {
                MaterialMessageBox.ShowError($"Failed to load suppliers: {ex.Message}", "Data Load Error");
                btnSave.Enabled = false;
            }


            if (_isEditMode)
            {
                txtNumber.Text = _currentDto.ContractNumber;
                dtpStart.Value = _currentDto.StartDate;

                if (_currentDto.EndDate.HasValue)
                {
                    dtpEnd.Value = _currentDto.EndDate.Value;
                    chkEndDate.Checked = true;
                }
                else
                {
                    dtpEnd.Value = DateTime.Today;
                    chkEndDate.Checked = false;
                }

                if (_currentDto.RenewalDate.HasValue)
                {
                    dtpRenewal.Value = _currentDto.RenewalDate.Value;
                    chkRenewalDate.Checked = true;
                }
                else
                {
                    dtpRenewal.Value = DateTime.Today;
                    chkRenewalDate.Checked = false;
                }

                txtTerms.Text = _currentDto.Terms;

                txtNumber.ReadOnly = true;
                txtNumber.Enabled = false;

                // Select the correct supplier for editing
                if (_currentDto.SupplierID > 0)
                {
                    cbSupplier.SelectedValue = _currentDto.SupplierID;
                }
                cbSupplier.Enabled = false; // Supplier shouldn't change in edit mode
            }
            else // Add mode
            {
                dtpStart.Value = DateTime.Today;
                dtpEnd.Value = DateTime.Today;
                dtpRenewal.Value = DateTime.Today;
                chkEndDate.Checked = false;
                chkRenewalDate.Checked = false;

                // Ensure a supplier is selected for new contracts
                cbSupplier.SelectedIndex = 0; // Select the first supplier by default
            }

            dtpEnd.Enabled = chkEndDate.Checked;
            dtpRenewal.Enabled = chkRenewalDate.Checked;

            // Ensure event handlers are hooked up (if not done in designer)
            if (this.chkEndDate != null) this.chkEndDate.CheckedChanged += new System.EventHandler(this.chkEndDate_CheckedChanged);
            if (this.chkRenewalDate != null) this.chkRenewalDate.CheckedChanged += new System.EventHandler(this.chkRenewalDate_CheckedChanged);
        }

        private void chkEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpEnd.Enabled = chkEndDate.Checked;
        }

        private void chkRenewalDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpRenewal.Enabled = chkRenewalDate.Checked;
        }

        #region Validation
        private void txtNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtNumber, "Contract number is required.");
            }
            else
            {
                errorProvider.SetError(txtNumber, "");
            }
        }

        private void txtTerms_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTerms.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtTerms, "Terms cannot be empty.");
            }
            else
            {
                errorProvider.SetError(txtTerms, "");
            }
        }

        private void cbSupplier_Validating(object sender, CancelEventArgs e)
        {
            if (cbSupplier.SelectedValue == null)
            {
                e.Cancel = true;
                errorProvider.SetError(cbSupplier, "Please select a supplier.");
            }
            else
            {
                errorProvider.SetError(cbSupplier, "");
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Add cbSupplier to validation check
            if (!ValidateChildren())
            {
                MaterialMessageBox.Show("Please correct the highlighted validation errors before saving.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dtoToSave = _isEditMode ? _currentDto : new SupplierContractDetailDTO();

            // SupplierID is critical for insertion
            if (cbSupplier.SelectedValue == null || (int)cbSupplier.SelectedValue <= 0) // MODIFIED
            {
                MaterialMessageBox.Show("Please select a valid supplier.", "Missing Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            


            if (!_isEditMode)
            {
                dtoToSave.SupplierID = (int)cbSupplier.SelectedValue; // Get the actual SupplierID!
                dtoToSave.ContractNumber = txtNumber.Text.Trim();
            }
            // For edit mode, SupplierID is already in _currentDto and usually not changed.
            // If it could be changed, you'd update dtoToSave.SupplierID here too.

            dtoToSave.StartDate = dtpStart.Value.Date;
            dtoToSave.EndDate = chkEndDate.Checked ? dtpEnd.Value.Date : (DateTime?)null;
            dtoToSave.RenewalDate = chkRenewalDate.Checked ? dtpRenewal.Value.Date : (DateTime?)null;
            dtoToSave.Terms = txtTerms.Text.Trim();

            bool success = false;
            try
            {
                success = _isEditMode
                    ? _bll.Update(dtoToSave)
                    : _bll.Insert(dtoToSave);
            }
            catch (Exception ex)
            {
                // The DAO now provides more specific messages for DbEntityValidationException
                // but the generic "unexpected error" from DAO still comes here if it's not a validation error.
                MaterialMessageBox.ShowError($"Save failed: {ex.Message}", "Database Error");
                return;
            }

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MaterialMessageBox.Show("The operation did not succeed. No changes were applied.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    public static class MaterialMessageBox
    {
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public static DialogResult ShowError(string text, string caption = "Error")
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}