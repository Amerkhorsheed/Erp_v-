using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class SupplierCommunicationForm : MaterialForm
    {
        private readonly SupplierCommunicationBLL _service = new SupplierCommunicationBLL();
        private List<SupplierCommunicationDetailDTO> _allComms;

        public SupplierCommunicationForm()
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
                Primary.Indigo500, Primary.Indigo700,
                Primary.Indigo100, Accent.DeepOrange200,
                TextShade.WHITE
            );
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadCommunications();
        }

        private void ConfigureGrid()
        {
            dgvCommunications.SuspendLayout();
            dgvCommunications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCommunications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCommunications.MultiSelect = false;
            dgvCommunications.ReadOnly = true;
            dgvCommunications.AllowUserToAddRows = false;
            dgvCommunications.RowHeadersVisible = false;
            dgvCommunications.ResumeLayout();
        }

        private void LoadCommunications()
        {
            var dto = _service.Select();
            _allComms = dto.Communications;
            dgvCommunications.DataSource = _allComms;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            void Hide(string col) { if (dgvCommunications.Columns[col] != null) dgvCommunications.Columns[col].Visible = false; }
            void Header(string c, string t) { if (dgvCommunications.Columns[c] != null) dgvCommunications.Columns[c].HeaderText = t; }

            Hide("CommunicationID");
            Hide("SupplierID");
            Header("SupplierName", "Supplier Name"); // Add this line
            Header("CommunicationDate", "Date");
            Header("Type", "Comm. Type");
            Header("Subject", "Subject");
            Header("Content", "Details");
            Header("ReferenceLink", "Link");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new SupplierCommunicationAddForm())
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadCommunications();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCommunications.CurrentRow == null)
            {
                MaterialMessageBox.Show("Please select a communication first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var selected = (SupplierCommunicationDetailDTO)dgvCommunications.CurrentRow.DataBoundItem;
            using (var dlg = new SupplierCommunicationAddForm(selected))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadCommunications();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCommunications.CurrentRow == null) return;
            var dto = (SupplierCommunicationDetailDTO)dgvCommunications.CurrentRow.DataBoundItem;
            var result = MaterialMessageBox.Show(
                $"Delete communication \"{dto.Subject}\"?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                _service.Delete(dto);
                LoadCommunications();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadCommunications();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var term = txtSearch.Text.Trim().ToLower();
            dgvCommunications.DataSource = string.IsNullOrEmpty(term)
                ? _allComms
                : _allComms.Where(c =>
                    c.Subject.ToLower().Contains(term) ||
                    c.Type.ToLower().Contains(term) ||
                    c.Content.ToLower().Contains(term) ||
                    (c.SupplierName != null && c.SupplierName.ToLower().Contains(term)) // Add this line for supplier name search
                ).ToList();
            ApplyColumnSettings();
        }

        private void dgvCommunications_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCommunications.Columns[e.ColumnIndex].Name != "ReferenceLink") return;
            var dto = (SupplierCommunicationDetailDTO)dgvCommunications.Rows[e.RowIndex].DataBoundItem;
            if (!string.IsNullOrWhiteSpace(dto.ReferenceLink))
            {
                Process.Start(new ProcessStartInfo(dto.ReferenceLink) { UseShellExecute = true });
            }
        }

        private void SupplierCommunicationForm_Load(object sender, EventArgs e)
        {

        }
    }
}