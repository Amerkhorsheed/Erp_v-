using DevExpress.XtraEditors;
using ClosedXML.Excel;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmSalesExport : XtraForm
    {
        // Business logic and data objects.
        private SalesBLL _salesBLL = new SalesBLL();
        private SalesDTO _salesDTO = new SalesDTO();
        private List<SalesDetailDTO> _salesList = new List<SalesDetailDTO>();

        // Holds the current row's details.
        private SalesDetailDTO detail = new SalesDetailDTO();

        public FrmSalesExport()
        {
            InitializeComponent();
            ApplyCustomStyling();
        }

        private async void FrmSalesExport_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadSalesAsync();
                ConfigureDataGrid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading sales data:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads the full list of sales asynchronously.
        /// </summary>
        private async Task LoadSalesAsync()
        {
            await Task.Run(() =>
            {
                _salesDTO = _salesBLL.Select();
                _salesList = _salesDTO.Sales;
            });

            dgvSales.Invoke(new Action(() =>
            {
                dgvSales.DataSource = _salesList;
            }));
        }

    
        private void ConfigureDataGrid()
        {
            if (dgvSales.Columns.Count > 0)
            {
                // Set header texts for the desired columns.
                dgvSales.Columns[0].HeaderText = "Customer Name";
                dgvSales.Columns[1].HeaderText = "Product Name";
                dgvSales.Columns[2].HeaderText = "Category Name";
                dgvSales.Columns[6].HeaderText = "Sales Amount";
                dgvSales.Columns[7].HeaderText = "Price";
                dgvSales.Columns[8].HeaderText = "Sales Date";

                // Hide columns not to be shown.
              
                int[] hideColumns = { 3, 4, 5, 9, 10, 11, 12, 13, 14, 15 , 16 , 17 };
                foreach (int colIndex in hideColumns)
                {
                    if (dgvSales.Columns.Count > colIndex)
                        dgvSales.Columns[colIndex].Visible = false;
                }
            }
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Applies modern styling to form controls.
        /// </summary>
        private void ApplyCustomStyling()
        {
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);

            // Style DataGridView header.
            dgvSales.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            dgvSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSales.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Style buttons.
            StyleButton(btnSearch, Color.FromArgb(0, 122, 204));
            StyleButton(btnClear, Color.Gray);
            StyleButton(btnExport, Color.FromArgb(0, 122, 204));
        }

        /// <summary>
        /// Applies consistent styling to a DevExpress SimpleButton.
        /// </summary>
        private void StyleButton(SimpleButton button, Color backColor)
        {
            if (button != null)
            {
                button.Appearance.BackColor = backColor;
                button.Appearance.ForeColor = Color.White;
                button.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                button.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            }
        }

        /// <summary>
        /// Filters the sales list based on user input.
        /// (You can expand this method with additional criteria as needed.)
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalesDetailDTO> filteredList = _salesList;

            // Filter by Customer Name.
            if (!string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                string custFilter = txtCustomerName.Text.Trim().ToLower();
                filteredList = filteredList.Where(x => x.CustomerName.ToLower().Contains(custFilter)).ToList();
            }

            // Filter by Sales Amount Range.
            if (!string.IsNullOrWhiteSpace(txtMinSales.Text) && int.TryParse(txtMinSales.Text, out int minSales))
            {
                filteredList = filteredList.Where(x => x.SalesAmount >= minSales).ToList();
            }
            if (!string.IsNullOrWhiteSpace(txtMaxSales.Text) && int.TryParse(txtMaxSales.Text, out int maxSales))
            {
                filteredList = filteredList.Where(x => x.SalesAmount <= maxSales).ToList();
            }

            // Filter by Date Range.
            if (chkDateRange.Checked)
            {
                DateTime startDate = dpStart.Value.Date;
                DateTime endDate = dpEnd.Value.Date;
                filteredList = filteredList.Where(x => x.SalesDate.Date >= startDate && x.SalesDate.Date <= endDate).ToList();
            }

            dgvSales.DataSource = filteredList;
        }

        /// <summary>
        /// Clears filter inputs and resets the DataGridView.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = string.Empty;
            txtMinSales.Text = string.Empty;
            txtMaxSales.Text = string.Empty;
            chkDateRange.Checked = false;
            dpStart.Value = DateTime.Today;
            dpEnd.Value = DateTime.Today;
            dgvSales.DataSource = _salesList;
        }

        /// <summary>
        /// Exports the current (filtered) sales data to an Excel file.
        /// Only the visible six columns are exported.
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Assuming the data source is a list.
                if (dgvSales.DataSource is List<SalesDetailDTO> list && list.Any())
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
                        sfd.Title = "Export Sales Data to Excel";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            using (var workbook = new XLWorkbook())
                            {
                                var worksheet = workbook.Worksheets.Add("SalesData");

                                // Create header row for the first 6 columns.
                                worksheet.Cell(1, 1).Value = "Customer Name";
                                worksheet.Cell(1, 2).Value = "Product Name";
                                worksheet.Cell(1, 3).Value = "Category Name";
                                worksheet.Cell(1, 4).Value = "Sales Amount";
                                worksheet.Cell(1, 5).Value = "Price";
                                worksheet.Cell(1, 6).Value = "Sales Date";

                                // Apply header styling.
                                var headerRange = worksheet.Range(1, 1, 1, 6);
                                headerRange.Style.Font.Bold = true;
                                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#007ACC");
                                headerRange.Style.Font.FontColor = XLColor.White;

                                int row = 2;
                                foreach (var sale in list)
                                {
                                    worksheet.Cell(row, 1).Value = sale.CustomerName;
                                    worksheet.Cell(row, 2).Value = sale.ProductName;
                                    worksheet.Cell(row, 3).Value = sale.CategoryName;
                                    worksheet.Cell(row, 4).Value = sale.SalesAmount;
                                    worksheet.Cell(row, 5).Value = sale.Price;
                                    worksheet.Cell(row, 6).Value = sale.SalesDate.ToShortDateString();
                                    row++;
                                }

                                workbook.SaveAs(sfd.FileName);
                            }
                            XtraMessageBox.Show("Sales data exported successfully.",
                                "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("No sales data available to export.",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error exporting data:\n{ex.Message}",
                    "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the RowEnter event.
        /// Retrieves additional details (even from hidden columns) using specific cell indexes.
        /// </summary>
        private void dgvSales_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Create a new instance for each row entry.
            detail = new SalesDetailDTO();

            // IMPORTANT: The cell indexes below are based on the full data source.
            // Adjust these indexes if your data source column order changes.
            try
            {
                detail.SalesID = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells[10].Value);
                detail.ProductID = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells[4].Value);
                detail.CustomerName = dgvSales.Rows[e.RowIndex].Cells[0].Value?.ToString();
                detail.ProductName = dgvSales.Rows[e.RowIndex].Cells[1].Value?.ToString();
                detail.Price = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells[7].Value);
                detail.SalesAmount = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells[6].Value);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error retrieving row details:\n{ex.Message}",
                    "Row Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
