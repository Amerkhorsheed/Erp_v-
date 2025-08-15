using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq; // Added for LINQ operations
using System.Text;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SupplierPerformanceAddForm : MaterialForm
    {
        private readonly SupplierPerformanceBLL _bll = new SupplierPerformanceBLL();
        private readonly bool _isEdit;
        private SupplierPerformanceDetailDTO _dto;
        private List<SupplierDetailDTO> _suppliers; // New: To store the list of suppliers

        public SupplierPerformanceAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _isEdit = false;
            Text = "Add Performance";
        }

        public SupplierPerformanceAddForm(SupplierPerformanceDetailDTO dto) : this()
        {
            _isEdit = true;
            _dto = dto;
            Text = "Edit Performance";
        }

        private void InitializeMaterialSkin()
        {
            var mgr = MaterialSkinManager.Instance;
            mgr.AddFormToManage(this);
            mgr.Theme = MaterialSkinManager.Themes.LIGHT;
            mgr.ColorScheme = new ColorScheme(
                Primary.Pink500, Primary.Pink700,
                Primary.Pink100, Accent.Pink200,
                TextShade.WHITE
            );
        }

        private void SupplierPerformanceAddForm_Load(object sender, EventArgs e)
        {
            // Fetch suppliers and bind to ComboBox
            _suppliers = _bll.GetSuppliersForSelection();
            if (!_suppliers.Any())
            {
                MessageBox.Show("No suppliers available. Please add suppliers before recording performance.", "No Suppliers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false; // Disable save if no suppliers to associate with
                cmbSupplier.Enabled = false;
                return;
            }

            cmbSupplier.DataSource = _suppliers;
            cmbSupplier.DisplayMember = "SupplierName"; // Assuming 'SupplierName' is the property in SupplierDetailDTO
            cmbSupplier.ValueMember = "SupplierID";     // Assuming 'SupplierID' is the primary key in SupplierDetailDTO

            if (_isEdit)
            {
                // Pre-fill fields for editing
                dtpDate.Value = _dto.EvaluationDate;
                numScore.Value = _dto.Score;
                txtParameters.Text = _dto.ParameterDetails;
                txtComments.Text = _dto.Comments;

                // Select the correct supplier in the ComboBox
                var selectedSupplier = _suppliers.FirstOrDefault(s => s.SupplierID == _dto.SupplierID);
                if (selectedSupplier != null)
                {
                    cmbSupplier.SelectedValue = selectedSupplier.SupplierID;
                }
                else
                {
                    // Handle cases where the original supplier might have been deleted
                    MessageBox.Show("The original supplier for this performance record could not be found. Please select an existing supplier.", "Supplier Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbSupplier.SelectedIndex = -1; // No supplier selected
                }
            }
            else
            {
                // Default values for new entry
                dtpDate.Value = DateTime.Today;
                numScore.Value = 0;
                cmbSupplier.SelectedIndex = 0; // Select the first supplier by default
            }
        }

        #region Validation
        private void cmbSupplier_Validating(object sender, CancelEventArgs e)
        {
            if (cmbSupplier.SelectedValue == null)
            {
                e.Cancel = true;
                errorProvider.SetError(cmbSupplier, "Please select a supplier.");
            }
            else
            {
                errorProvider.SetError(cmbSupplier, "");
            }
        }

        private void numScore_Validating(object sender, CancelEventArgs e)
        {
            if (numScore.Value < 0 || numScore.Value > 100)
            {
                e.Cancel = true;
                errorProvider.SetError(numScore, "Score must be between 0 and 100.");
            }
            else
            {
                errorProvider.SetError(numScore, "");
            }
        }

        private void txtParameters_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtParameters.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtParameters, "Parameters are required.");
            }
            else
            {
                errorProvider.SetError(txtParameters, "");
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate all children controls before attempting to save
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show("Please fix validation errors.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create or update DTO
            var dto = _isEdit ? _dto : new SupplierPerformanceDetailDTO();

            // Assign the selected SupplierID from the ComboBox
            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show("A supplier must be selected.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dto.SupplierID = (int)cmbSupplier.SelectedValue; // This is crucial for the foreign key

            dto.EvaluationDate = dtpDate.Value.Date;
            dto.Score = numScore.Value;
            dto.ParameterDetails = txtParameters.Text.Trim();
            dto.Comments = txtComments.Text.Trim();

            bool success;
            try
            {
                success = _isEdit
                    ? _bll.Update(dto)
                    : _bll.Insert(dto);
            }
            catch (Exception ex)
            {
                // Provide detailed error message for debugging
                var sb = new StringBuilder($"Save failed: {ex.Message}");
                var inner = ex.InnerException;
                while (inner != null)
                {
                    sb.AppendLine($"\nInner: {inner.Message}");
                    inner = inner.InnerException;
                }
                MessageBox.Show(sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Operation did not succeed. No changes were saved.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}