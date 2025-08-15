// File: CustomerDocumentForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class CustomerDocumentForm : MaterialForm
    {
        private readonly CustomerDocumentBLL _documentService = new CustomerDocumentBLL();
        private List<CustomerDocumentDTO> _allDocs;

        public CustomerDocumentForm()
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
                Primary.Red500, Primary.Red700,
                Primary.Red100, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadDocuments();
        }

        private void ConfigureGrid()
        {
            dgvDocuments.SuspendLayout();
            dgvDocuments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDocuments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocuments.MultiSelect = false;
            dgvDocuments.ReadOnly = true;
            dgvDocuments.AllowUserToAddRows = false;
            dgvDocuments.RowHeadersVisible = false;
            dgvDocuments.ResumeLayout();
        }

        private void LoadDocuments()
        {
            var result = _documentService.Select();
            _allDocs = result.Documents;
            dgvDocuments.DataSource = _allDocs;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            void Hide(string col) { if (dgvDocuments.Columns[col] != null) dgvDocuments.Columns[col].Visible = false; }
            void Header(string c, string t) { if (dgvDocuments.Columns[c] != null) dgvDocuments.Columns[c].HeaderText = t; }

            Hide("ID");
            Hide("CustomerID");
            Header("CustomerName", "Customer Name");
            Header("FileName", "Document");
            // assume column named FilePath remains visible
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            using (var dlg = new CustomerDocumentAddForm())
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadDocuments();
        }

        private void OnEditClick(object sender, EventArgs e)
        {
            if (dgvDocuments.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a document first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dto = (CustomerDocumentDTO)dgvDocuments.CurrentRow.DataBoundItem;
            using (var dlg = new CustomerDocumentAddForm(dto))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadDocuments();
        }

        private async void OnDeleteClickAsync(object sender, EventArgs e)
        {
            if (dgvDocuments.CurrentRow == null) return;
            var dto = (CustomerDocumentDTO)dgvDocuments.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Delete '{dto.FileName}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );
            if (confirm == DialogResult.Yes)
            {
                await _documentService.DeleteAsync(dto);
                LoadDocuments();
            }
        }

        private void OnRefreshClick(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            LoadDocuments();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var term = searchTextBox.Text.Trim().ToLower();
            dgvDocuments.DataSource = string.IsNullOrEmpty(term)
                ? _allDocs
                : _allDocs.Where(d =>
                    (!string.IsNullOrEmpty(d.CustomerName) && d.CustomerName.ToLower().Contains(term)) ||
                    (!string.IsNullOrEmpty(d.FileName) && d.FileName.ToLower().Contains(term))
                ).ToList();
            ApplyColumnSettings();
        }

        private void OnCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvDocuments.Columns[e.ColumnIndex];
            if (col == null || col.Name != "FilePath") return;
            if (dgvDocuments.Rows[e.RowIndex].DataBoundItem is CustomerDocumentDTO dto)
                OpenFile(dto.FilePath);
        }

        private void OpenFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show($"File not found: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var psi = new ProcessStartInfo(path) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open file:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
