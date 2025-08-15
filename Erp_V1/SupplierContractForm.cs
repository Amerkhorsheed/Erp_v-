// File: SupplierContractForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SupplierContractForm : MaterialForm
    {
        private readonly SupplierContractBLL _service = new SupplierContractBLL();
        private List<SupplierContractDetailDTO> _allContracts;

        public SupplierContractForm()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            Load += OnLoad;
        }

        private void InitializeMaterialSkin()
        {
            var mgr = MaterialSkinManager.Instance;
            mgr.AddFormToManage(this);
            mgr.Theme = MaterialSkinManager.Themes.LIGHT;
            mgr.ColorScheme = new ColorScheme(
                Primary.Blue500, Primary.Blue700,
                Primary.Blue100, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadContracts();
        }

        private void ConfigureGrid()
        {
            dgvContracts.SuspendLayout();
            dgvContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContracts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContracts.MultiSelect = false;
            dgvContracts.ReadOnly = true;
            dgvContracts.AllowUserToAddRows = false;
            dgvContracts.RowHeadersVisible = false;
            dgvContracts.ResumeLayout();
        }

        private void LoadContracts()
        {
            var dto = _service.Select();
            _allContracts = dto.Contracts;
            dgvContracts.DataSource = _allContracts;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            void Hide(string col) { if (dgvContracts.Columns[col] != null) dgvContracts.Columns[col].Visible = false; }
            void Header(string c, string t) { if (dgvContracts.Columns[c] != null) dgvContracts.Columns[c].HeaderText = t; }

            Hide("ContractID");
            Hide("SupplierID");
            Header("SupplierName", "Supplier Name"); // Display Supplier Name
            Header("ContractNumber", "Number");
            Header("StartDate", "Start");
            Header("EndDate", "End");
            Header("RenewalDate", "Renewal");
            Header("Terms", "Terms");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new SupplierContractAddForm())
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadContracts();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvContracts.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a contract first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dto = (SupplierContractDetailDTO)dgvContracts.CurrentRow.DataBoundItem;
            using (var dlg = new SupplierContractAddForm(dto))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadContracts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvContracts.CurrentRow == null) return;
            var dto = (SupplierContractDetailDTO)dgvContracts.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Delete contract \"{dto.ContractNumber}\"?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );
            if (confirm == DialogResult.Yes)
            {
                _service.Delete(dto);
                LoadContracts();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadContracts();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var term = txtSearch.Text.Trim().ToLower();
            dgvContracts.DataSource = string.IsNullOrEmpty(term)
                ? _allContracts
                : _allContracts.Where(c =>
                    (!string.IsNullOrEmpty(c.ContractNumber) && c.ContractNumber.ToLower().Contains(term)) ||
                    (!string.IsNullOrEmpty(c.Terms) && c.Terms.ToLower().Contains(term)) ||
                    (!string.IsNullOrEmpty(c.SupplierName) && c.SupplierName.ToLower().Contains(term)) // Added search by SupplierName
                ).ToList();
            ApplyColumnSettings();
        }

        private void dgvContracts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvContracts.Columns[e.ColumnIndex].Name != "Terms") return;
            var dto = (SupplierContractDetailDTO)dgvContracts.Rows[e.RowIndex].DataBoundItem;
            MaterialMessageBox.Show(dto.Terms, "Contract Terms", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SupplierContractForm_Load(object sender, EventArgs e)
        {

        }
    }
}