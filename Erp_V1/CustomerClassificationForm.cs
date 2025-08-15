// File: CustomerClassificationForm.cs
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using MaterialSkin; // Add this using statement
using MaterialSkin.Controls; // Add this using statement
using System;
using System.Collections.Generic; // Added for list filtering
using System.Drawing; // Make sure this is present for Color and Font
using System.Linq; // Added for LINQ filtering
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    // Change base class to MaterialForm
    public partial class CustomerClassificationForm : MaterialForm
    {
        private readonly CustomerClassificationBLL _bll = new CustomerClassificationBLL();
        private List<CustomerClassificationDTO> _allClassifications; // Store all data for filtering

        public CustomerClassificationForm()
        {
            InitializeComponent();
            InitializeMaterialSkin(); // Initialize MaterialSkin
            this.Load += CustomerClassificationForm_Load;
        }

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; // Or DARK
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Indigo500, Primary.Indigo700,
                Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
        }

        private void CustomerClassificationForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadData();
        }

        private void ConfigureGrid()
        {
            dgvClassifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClassifications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClassifications.MultiSelect = false;
            dgvClassifications.ReadOnly = true;
            dgvClassifications.AllowUserToAddRows = false;
            dgvClassifications.RowHeadersVisible = false;

            // Apply Material Design styling to DataGridView
            dgvClassifications.EnableHeadersVisualStyles = false;
            dgvClassifications.ColumnHeadersDefaultCellStyle.BackColor = MaterialSkinManager.Instance.ColorScheme.PrimaryColor;
            dgvClassifications.ColumnHeadersDefaultCellStyle.ForeColor = MaterialSkinManager.Instance.ColorScheme.TextColor;
            dgvClassifications.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dgvClassifications.DefaultCellStyle.Font = new Font("Roboto", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgvClassifications.RowsDefaultCellStyle.BackColor = MaterialSkinManager.Instance.BackgroundColor;

            // FIX: Removed Luminance check, simplified alternating row color for compatibility
            dgvClassifications.AlternatingRowsDefaultCellStyle.BackColor = ControlPaint.Light(MaterialSkinManager.Instance.BackgroundColor, 0.05f); // Make alternating rows slightly lighter

            dgvClassifications.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Only horizontal lines

            // FIX: Changed GridColor to an opaque color as transparent colors are not allowed
            dgvClassifications.GridColor = Color.Gainsboro; // A good, solid light gray for grid lines

            dgvClassifications.BorderStyle = BorderStyle.None; // Remove default border
        }

        private void LoadData()
        {
            // Store all data
            _allClassifications = _bll.Select().Classifications;
            // Display all data initially
            dgvClassifications.DataSource = _allClassifications;

            ApplyGridColumnVisibilityAndHeaders();
        }

        private void ApplyGridColumnVisibilityAndHeaders()
        {
            // Hide the ID and CustomerID columns
            if (dgvClassifications.Columns["ID"] != null)
                dgvClassifications.Columns["ID"].Visible = false;

            if (dgvClassifications.Columns["CustomerID"] != null)
                dgvClassifications.Columns["CustomerID"].Visible = false;

            // Set header text for CustomerName
            if (dgvClassifications.Columns["CustomerName"] != null)
                dgvClassifications.Columns["CustomerName"].HeaderText = "Customer Name";
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new CustomerClassificationAddForm())
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvClassifications.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a classification to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dto = (CustomerClassificationDTO)dgvClassifications.CurrentRow.DataBoundItem;
            using (var frm = new CustomerClassificationAddForm(dto))
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClassifications.CurrentRow == null)
            {
                MaterialMessageBox.Show("Select a classification to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dto = (CustomerClassificationDTO)dgvClassifications.CurrentRow.DataBoundItem;
            var confirm = MaterialMessageBox.Show(
                $"Are you sure you want to delete the classification for '{dto.CustomerName}' ({dto.Tier})?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool ok = await _bll.DeleteAsync(dto);
            if (!ok)
                MaterialMessageBox.Show("Delete failed. Please try again.", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            searchTextBox.Clear(); // Clear search on refresh
        }

        // Search functionality
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void FilterData()
        {
            string searchText = searchTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvClassifications.DataSource = _allClassifications; // Show all if search is empty
            }
            else
            {
                var filteredList = _allClassifications
                    // FIX: Changed Contains for broader .NET Framework compatibility (case-insensitive)
                    .Where(c => c.CustomerName != null && c.CustomerName.ToLower().Contains(searchText.ToLower()))
                    .ToList();
                dgvClassifications.DataSource = filteredList;
            }
            ApplyGridColumnVisibilityAndHeaders(); // Re-apply visibility after filtering
        }
    }
}