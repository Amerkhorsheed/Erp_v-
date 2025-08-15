// File: SupplierPerformanceForm.cs
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
    public partial class SupplierPerformanceForm : MaterialForm
    {
        private readonly SupplierPerformanceBLL _service = new SupplierPerformanceBLL();
        private List<SupplierPerformanceDetailDTO> _allRecords;

        public SupplierPerformanceForm()
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
                Primary.DeepOrange500, Primary.DeepOrange700,
                Primary.DeepOrange100, Accent.Orange200,
                TextShade.WHITE
            );
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadPerformances();
        }

        private void ConfigureGrid()
        {
            dgvPerformance.SuspendLayout();
            dgvPerformance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPerformance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPerformance.MultiSelect = false;
            dgvPerformance.ReadOnly = true;
            dgvPerformance.AllowUserToAddRows = false;
            dgvPerformance.RowHeadersVisible = false;
            dgvPerformance.ResumeLayout();
        }

        private void LoadPerformances()
        {
            var dto = _service.Select();
            _allRecords = dto.Performances;
            dgvPerformance.DataSource = _allRecords;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            void Hide(string col) { if (dgvPerformance.Columns[col] != null) dgvPerformance.Columns[col].Visible = false; }
            void Header(string c, string t) { if (dgvPerformance.Columns[c] != null) dgvPerformance.Columns[c].HeaderText = t; }

            Hide("PerformanceID");
            Hide("SupplierID");
            Header("SupplierName", "Supplier Name"); // Display Supplier Name
            Header("EvaluationDate", "Date");
            Header("Score", "Score");
            Header("ParameterDetails", "Parameters");
            Header("Comments", "Comments");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new SupplierPerformanceAddForm())
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadPerformances();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPerformance.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a record first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dto = (SupplierPerformanceDetailDTO)dgvPerformance.CurrentRow.DataBoundItem;
            using (var dlg = new SupplierPerformanceAddForm(dto))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadPerformances();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPerformance.CurrentRow == null) return;
            var dto = (SupplierPerformanceDetailDTO)dgvPerformance.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Delete evaluation on {dto.EvaluationDate:d}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );
            if (confirm == DialogResult.Yes)
            {
                _service.Delete(dto);
                LoadPerformances();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadPerformances();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var term = txtSearch.Text.Trim().ToLower();
            dgvPerformance.DataSource = string.IsNullOrEmpty(term)
                ? _allRecords
                : _allRecords.Where(p =>
                      (!string.IsNullOrEmpty(p.SupplierName) && p.SupplierName.ToLower().Contains(term)) || // Added search by SupplierName
                      p.Score.ToString().Contains(term) ||
                      (!string.IsNullOrEmpty(p.ParameterDetails) && p.ParameterDetails.ToLower().Contains(term)) ||
                      (!string.IsNullOrEmpty(p.Comments) && p.Comments.ToLower().Contains(term))
                  ).ToList();
            ApplyColumnSettings();
        }

        private void dgvPerformance_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvPerformance.Columns[e.ColumnIndex];
            if (col.Name == "Comments")
            {
                var dto = (SupplierPerformanceDetailDTO)dgvPerformance.Rows[e.RowIndex].DataBoundItem;
                MaterialMessageBox.Show(dto.Comments, "Comments", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}