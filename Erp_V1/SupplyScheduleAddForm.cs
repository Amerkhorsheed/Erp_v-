// File: SupplyScheduleAddForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SupplyScheduleAddForm : MaterialForm
    {
        private readonly SupplyScheduleBLL _bll;
        private readonly SupplierBLL _supplierBll;
        private readonly SupplierContractBLL _contractBll;
        private bool _isEdit;
        private SupplyScheduleDetailDTO _dto;
        private List<SupplierDetailDTO> _suppliers;
        private List<SupplierContractDetailDTO> _contracts;

        public SupplyScheduleAddForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();

            _bll = new SupplyScheduleBLL();
            _supplierBll = new SupplierBLL();
            _contractBll = new SupplierContractBLL();
            _isEdit = false;
            Text = "Add Supply Schedule";

            Load += SupplyScheduleAddForm_Load;
        }

        public SupplyScheduleAddForm(SupplyScheduleDetailDTO dto) : this()
        {
            _isEdit = true;
            _dto = dto;
            Text = "Edit Supply Schedule";
        }

        private void InitializeMaterialSkin()
        {
            var mgr = MaterialSkinManager.Instance;
            mgr.AddFormToManage(this);
            mgr.Theme = MaterialSkinManager.Themes.LIGHT;
            mgr.ColorScheme = new ColorScheme(
                Primary.Lime500, Primary.Lime700,
                Primary.Lime100, Accent.LightGreen200,
                TextShade.WHITE
            );
        }

        private void SupplyScheduleAddForm_Load(object sender, EventArgs e)
        {
            // Load suppliers
            _suppliers = _supplierBll.Select().Suppliers;
            cmbSupplier.DataSource = _suppliers;
            cmbSupplier.DisplayMember = nameof(SupplierDetailDTO.SupplierName);
            cmbSupplier.ValueMember = nameof(SupplierDetailDTO.SupplierID);
            cmbSupplier.SelectedIndex = -1;

            if (_isEdit)
            {
                // Pre‐select supplier & populate contracts
                var selSup = _suppliers.FirstOrDefault(s => s.SupplierID == _dto.SupplierID);
                cmbSupplier.SelectedItem = selSup;
                LoadContractsForSelectedSupplier();
                dtpExpected.Value = _dto.ExpectedDate;
                numQuantity.Value = _dto.Quantity;
                txtStatus.Text = _dto.Status;
            }
            else
            {
                dtpExpected.Value = DateTime.Today;
                numQuantity.Value = 1;
            }
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadContractsForSelectedSupplier();
        }

        private void LoadContractsForSelectedSupplier()
        {
            errorProvider.SetError(cmbContract, "");  // clear any prior error

            if (!(cmbSupplier.SelectedItem is SupplierDetailDTO sup))
            {
                cmbContract.DataSource = null;
                return;
            }

            // Fetch contracts for chosen supplier
            _contracts = _contractBll
                .Select()
                .Contracts
                .Where(c => c.SupplierID == sup.SupplierID)
                .ToList();

            cmbContract.DataSource = _contracts;
            cmbContract.DisplayMember = nameof(SupplierContractDetailDTO.ContractNumber);
            cmbContract.ValueMember = nameof(SupplierContractDetailDTO.ContractID);

            if (_isEdit)
            {
                var selCtr = _contracts.FirstOrDefault(c => c.ContractID == _dto.ContractID);
                cmbContract.SelectedItem = selCtr;
            }
            else
            {
                cmbContract.SelectedIndex = -1;
            }
        }

        #region Validation

        private void cmbSupplier_Validating(object sender, CancelEventArgs e)
        {
            if (!(cmbSupplier.SelectedItem is SupplierDetailDTO))
            {
                e.Cancel = true;
                errorProvider.SetError(cmbSupplier, "Please select a supplier.");
            }
            else
            {
                errorProvider.SetError(cmbSupplier, "");
            }
        }

        private void cmbContract_Validating(object sender, CancelEventArgs e)
        {
            if (!(cmbContract.SelectedItem is SupplierContractDetailDTO))
            {
                e.Cancel = true;
                errorProvider.SetError(cmbContract, "Please select a contract.");
            }
            else
            {
                errorProvider.SetError(cmbContract, "");
            }
        }

        private void numQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (numQuantity.Value <= 0)
            {
                e.Cancel = true;
                errorProvider.SetError(numQuantity, "Quantity must be positive.");
            }
            else
            {
                errorProvider.SetError(numQuantity, "");
            }
        }

        private void txtStatus_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStatus.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtStatus, "Status is required.");
            }
            else
            {
                errorProvider.SetError(txtStatus, "");
            }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show(
                    "Please correct validation errors.",
                    "Validation Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Map back from SelectedItem instead of SelectedValue
            var dto = _isEdit ? _dto : new SupplyScheduleDetailDTO();

            if (!(cmbSupplier.SelectedItem is SupplierDetailDTO selSup) ||
                !(cmbContract.SelectedItem is SupplierContractDetailDTO selCtr))
            {
                // This should never happen if validation passed, but just in case:
                MessageBox.Show("Invalid selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dto.SupplierID = selSup.SupplierID;
            dto.ContractID = selCtr.ContractID;
            dto.ExpectedDate = dtpExpected.Value.Date;
            dto.Quantity = (int)numQuantity.Value;
            dto.Status = txtStatus.Text.Trim();

            try
            {
                var success = _isEdit
                    ? _bll.Update(dto)
                    : _bll.Insert(dto);

                if (success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Operation did not succeed.",
                        "Failure",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Save failed:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
