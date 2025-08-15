using System;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraEditors;

namespace Erp_V1
{
    public partial class frmReturnSearch : XtraForm
    {
        private ReturnBLL returnBLL = new ReturnBLL();
        private ReturnDTO returnDTO = new ReturnDTO();

        public frmReturnSearch()
        {
            InitializeComponent();
        }

        private void frmReturnSearch_Load(object sender, EventArgs e)
        {
            try
            {
                // Set default date range: last month to today.
                dtpFrom.Value = DateTime.Today.AddMonths(-1);
                dtpTo.Value = DateTime.Today;

                // Load all return records along with categories and customers.
                returnDTO = returnBLL.Select();

                // Load and format the returns data.
                LoadReturnData();

                // Populate category combo box (if any categories exist).
                if (returnDTO.Categories != null && returnDTO.Categories.Count > 0)
                {
                    cmbCategory.DataSource = returnDTO.Categories;
                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "ID";
                    cmbCategory.SelectedIndex = -1;
                }

                // Populate customer grid for easy customer selection.
                if (returnDTO.Customers != null && returnDTO.Customers.Count > 0)
                {
                    dgvCustomers.DataSource = returnDTO.Customers;
                    // You might need to adjust the column headers and visibility here
                    // to match how it's done in FrmSales for gridCustomer.
                    // Assuming the first column is CustomerID and second is CustomerName.
                    if (dgvCustomers.Columns.Count > 0) dgvCustomers.Columns[0].Visible = false; // Hide ID if it exists
                    if (dgvCustomers.Columns.Count > 1) dgvCustomers.Columns[1].HeaderText = "Customer Name";
                    if (dgvCustomers.Columns.Count > 2) dgvCustomers.Columns[2].Visible = false;
                    if (dgvCustomers.Columns.Count > 3) dgvCustomers.Columns[3].Visible = false;
                    if (dgvCustomers.Columns.Count > 4) dgvCustomers.Columns[4].Visible = false;
                    if (dgvCustomers.Columns.Count > 5) dgvCustomers.Columns[5].Visible = false;
                    // You might want to show other relevant customer information as well.
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error loading return data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When a customer row is clicked, load the customer's name into the filter text box.
        private void dgvCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvCustomers.Rows.Count)
                    return;

                var row = dgvCustomers.Rows[e.RowIndex];
                // Assuming the first column is CustomerID and second column is CustomerName.
                if (row.Cells.Count > 1 && row.Cells[1].Value != null)
                {
                    txtCustomerName.Text = row.Cells[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error selecting customer: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filter the returns based on customer, product, category, and (optionally) date range.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var returns = returnDTO.Returns;

                // Filter by Customer Name (from textbox populated via customer grid).
                if (!string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    returns = returns.Where(r =>
                        r.CustomerName.IndexOf(txtCustomerName.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                // Filter by Product Name.
                if (!string.IsNullOrEmpty(txtProductName.Text))
                {
                    returns = returns.Where(r =>
                        r.ProductName.IndexOf(txtProductName.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                // Filter by Category if a category is selected.
                if (cmbCategory.SelectedIndex != -1)
                {
                    // Here, we assume that r.CategoryID is available in the return record.
                    int selectedCategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
                    returns = returns.Where(r => r.CategoryID == selectedCategoryId).ToList();
                }

                // Filter by Date Range if checked.
                if (chkFilterByDate.Checked)
                {
                    DateTime from = dtpFrom.Value.Date;
                    DateTime to = dtpTo.Value.Date;
                    returns = returns.Where(r => r.ReturnDate >= from && r.ReturnDate <= to).ToList();
                }

                dgvReturns.DataSource = returns; // Update the search results grid
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error during search: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Export grid contents to Excel.
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel(dgvReturns); // Export from the correct grid
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error exporting to Excel: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exports the contents of a DataGridView to Excel using Microsoft Office Interop.
        /// </summary>
        /// <param name="dgv">The DataGridView to export.</param>
        private void ExportToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                XtraMessageBox.Show("No data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                XtraMessageBox.Show("Excel is not properly installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            // Export header row.
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                xlWorkSheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }
            // Export data rows.
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    xlWorkSheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                }
            }
            xlApp.Visible = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = string.Empty;
            txtProductName.Text = string.Empty;
            cmbCategory.SelectedIndex = -1;
            chkFilterByDate.Checked = false;
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;

            // Reload all return data into dgvReturns, similar to form load
            LoadReturnData();
        }

        private void LoadReturnData()
        {
            var returnDto = returnBLL.Select();
            dgvReturns.DataSource = returnDto.Returns;
            if (dgvReturns.Columns["ReturnID"] != null)
                dgvReturns.Columns["ReturnID"].HeaderText = "Return ID";
            if (dgvReturns.Columns["ProductName"] != null)
                dgvReturns.Columns["ProductName"].HeaderText = "Product";
            if (dgvReturns.Columns["CustomerName"] != null)
                dgvReturns.Columns["CustomerName"].HeaderText = "Customer";
            if (dgvReturns.Columns["ReturnQuantity"] != null)
                dgvReturns.Columns["ReturnQuantity"].HeaderText = "Return Quantity";
            if (dgvReturns.Columns["ReturnDate"] != null)
                dgvReturns.Columns["ReturnDate"].HeaderText = "Return Date";
            if (dgvReturns.Columns["ReturnReason"] != null)
                dgvReturns.Columns["ReturnReason"].HeaderText = "Return Reason";

            // Assuming these are the correct indices of the columns you want to hide
            if (dgvReturns.Columns.Count > 0) dgvReturns.Columns[0].Visible = false;
            if (dgvReturns.Columns.Count > 1) dgvReturns.Columns[1].Visible = false;
            if (dgvReturns.Columns.Count > 2) dgvReturns.Columns[2].Visible = false;
            if (dgvReturns.Columns.Count > 3) dgvReturns.Columns[3].Visible = false;
            if (dgvReturns.Columns.Count > 10) dgvReturns.Columns[10].Visible = false;
        }
    }
}