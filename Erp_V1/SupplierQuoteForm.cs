// File: SupplierQuoteForm.cs
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
    public partial class SupplierQuoteForm : MaterialForm
    {
        private readonly SupplierQuoteBLL _service = new SupplierQuoteBLL();
        private List<SupplierQuoteDetailDTO> _allQuotes;

        public SupplierQuoteForm()
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
                Primary.Brown500, Primary.Brown700,
                Primary.Brown100, Accent.Orange200,
                TextShade.WHITE
            );
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadQuotes();
        }

        private void ConfigureGrid()
        {
            dgvQuotes.SuspendLayout();
            dgvQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvQuotes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQuotes.MultiSelect = false;
            dgvQuotes.ReadOnly = true;
            dgvQuotes.AllowUserToAddRows = false;
            dgvQuotes.RowHeadersVisible = false;
            dgvQuotes.ResumeLayout();
        }

        private void LoadQuotes()
        {
            var dto = _service.Select();
            _allQuotes = dto.Quotes;
            dgvQuotes.DataSource = _allQuotes;
            ApplyColumnSettings();
        }

        private void ApplyColumnSettings()
        {
            void Hide(string c) { if (dgvQuotes.Columns[c] != null) dgvQuotes.Columns[c].Visible = false; }
            void Header(string c, string t) { if (dgvQuotes.Columns[c] != null) dgvQuotes.Columns[c].HeaderText = t; }

            // Hide the specified IDs
            Hide("QuoteID");
            Hide("RequestID"); // Hide RequestID
            Hide("SupplierID"); // Hide SupplierID

            // Add and set header for SupplierName
            Header("SupplierName", "Supplier Name"); // Display Supplier Name

            // Existing headers
            Header("QuoteDate", "Date");
            Header("TotalAmount", "Amount");
            Header("Currency", "Currency");
            Header("Details", "Details");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new SupplierQuoteAddForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadQuotes();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvQuotes.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a quote first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dto = (SupplierQuoteDetailDTO)dgvQuotes.CurrentRow.DataBoundItem;

            using (var dlg = new SupplierQuoteAddForm(dto))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadQuotes();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvQuotes.CurrentRow == null) return;
            var dto = (SupplierQuoteDetailDTO)dgvQuotes.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Delete quote {dto.QuoteID}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );
            if (confirm == DialogResult.Yes)
            {
                _service.Delete(dto);
                LoadQuotes();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadQuotes();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var t = txtSearch.Text.Trim().ToLower();
            dgvQuotes.DataSource = string.IsNullOrEmpty(t)
                ? _allQuotes
                : _allQuotes.Where(q =>
                    (!string.IsNullOrEmpty(q.SupplierName) && q.SupplierName.ToLower().Contains(t)) || // Added search by SupplierName
                    q.RequestID.ToString().ToLower().Contains(t) || // Still allow searching by RequestID if needed
                    q.Currency.ToLower().Contains(t) ||
                    q.Details.ToLower().Contains(t) ||
                    q.TotalAmount.ToString().Contains(t) // Added searching by total amount
                ).ToList();
            ApplyColumnSettings();
        }

        private void dgvQuotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvQuotes.Columns[e.ColumnIndex].Name;
            if (col == "Details")
            {
                var dto = (SupplierQuoteDetailDTO)dgvQuotes.Rows[e.RowIndex].DataBoundItem;
                MaterialMessageBox.Show(dto.Details, "Quote Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SupplierQuoteForm_Load(object sender, EventArgs e)
        {
            // This event handler is currently empty and can be removed or used for additional load logic.
        }
    }
}