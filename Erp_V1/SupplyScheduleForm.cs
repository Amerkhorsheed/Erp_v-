// File: SupplyScheduleForm.cs
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
    public partial class SupplyScheduleForm : MaterialForm
    {
        private readonly SupplyScheduleBLL _service = new SupplyScheduleBLL();
        private List<SupplyScheduleDetailDTO> _allSchedules;

        public SupplyScheduleForm()
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
                Primary.Cyan500, Primary.Cyan700,
                Primary.Cyan100, Accent.Teal200,
                TextShade.WHITE
            );
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadSchedules();
        }

        private void ConfigureGrid()
        {
            dgvSchedules.SuspendLayout();
            dgvSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSchedules.MultiSelect = false;
            dgvSchedules.ReadOnly = true;
            dgvSchedules.AllowUserToAddRows = false;
            dgvSchedules.RowHeadersVisible = false;
            dgvSchedules.ResumeLayout();
        }

        private void LoadSchedules()
        {
            _allSchedules = _service.Select().Schedules;
            dgvSchedules.DataSource = _allSchedules;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            // Hide IDs
            foreach (var colName in new[] { "ScheduleID", "ContractID", "SupplierID" })
            {
                if (dgvSchedules.Columns.Contains(colName))
                    dgvSchedules.Columns[colName].Visible = false;
            }

            // Friendly headers
            if (dgvSchedules.Columns.Contains("SupplierName"))
                dgvSchedules.Columns["SupplierName"].HeaderText = "Supplier";
            if (dgvSchedules.Columns.Contains("ExpectedDate"))
                dgvSchedules.Columns["ExpectedDate"].HeaderText = "Expected";
            if (dgvSchedules.Columns.Contains("Quantity"))
                dgvSchedules.Columns["Quantity"].HeaderText = "Qty";
            if (dgvSchedules.Columns.Contains("Status"))
                dgvSchedules.Columns["Status"].HeaderText = "Status";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new SupplyScheduleAddForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadSchedules();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSchedules.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a schedule first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dto = (SupplyScheduleDetailDTO)dgvSchedules.CurrentRow.DataBoundItem;
            using (var dlg = new SupplyScheduleAddForm(dto))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadSchedules();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSchedules.CurrentRow == null) return;
            var dto = (SupplyScheduleDetailDTO)dgvSchedules.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Delete schedule on {dto.ExpectedDate:d} for {dto.SupplierName}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );
            if (confirm == DialogResult.Yes)
            {
                _service.Delete(dto);
                LoadSchedules();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadSchedules();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var term = txtSearch.Text.Trim().ToLower();
            var filtered = string.IsNullOrEmpty(term)
                ? _allSchedules
                : _allSchedules.Where(s =>
                    s.SupplierName.ToLower().Contains(term) ||
                    s.ExpectedDate.ToString("d").Contains(term) ||
                    s.Quantity.ToString().Contains(term) ||
                    (!string.IsNullOrEmpty(s.Status) && s.Status.ToLower().Contains(term))
                ).ToList();

            dgvSchedules.DataSource = filtered;
            ApplyColumnSettings();
        }

        private void dgvSchedules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvSchedules.Columns[e.ColumnIndex].Name == "Status")
            {
                var dto = (SupplyScheduleDetailDTO)dgvSchedules.Rows[e.RowIndex].DataBoundItem;
                MaterialMessageBox.Show(dto.Status, "Schedule Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Suppress the default DataGridView error dialog
        private void dgvSchedules_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Optional: log e.Exception here if desired
            e.ThrowException = false;
        }
    }
}
