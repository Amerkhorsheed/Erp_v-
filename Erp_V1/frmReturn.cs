using System;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmReturn : DevExpress.XtraEditors.XtraForm
    {
        private ReturnBLL returnBLL = new ReturnBLL();
        private SalesBLL salesBLL = new SalesBLL();

        // Holds the currently selected return record (for update/delete)
        private ReturnDetailDTO currentReturn = new ReturnDetailDTO();
        // Holds the selected sale record from which to process a return.
        private SalesDetailDTO selectedSale = new SalesDetailDTO();

        public frmReturn()
        {
            InitializeComponent();
        }

        private void frmReturn_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSalesData();
                LoadReturnData();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading return form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads sales records into dgvSales and adjusts column visibility.
        /// </summary>
        private void LoadSalesData()
        {
            var salesDto = salesBLL.Select();
            dgvSales.DataSource = salesDto.Sales;

            // Set header texts for clarity.
            if (dgvSales.Columns["SalesID"] != null)
                dgvSales.Columns["SalesID"].HeaderText = "Sale ID";
            if (dgvSales.Columns["ProductName"] != null)
                dgvSales.Columns["ProductName"].HeaderText = "Product";
            if (dgvSales.Columns["CustomerName"] != null)
                dgvSales.Columns["CustomerName"].HeaderText = "Customer";
            if (dgvSales.Columns["SalesAmount"] != null)
                dgvSales.Columns["SalesAmount"].HeaderText = "Quantity Sold";
            if (dgvSales.Columns["Price"] != null)
                dgvSales.Columns["Price"].HeaderText = "Sale Price";
            if (dgvSales.Columns["SalesDate"] != null)
                dgvSales.Columns["SalesDate"].HeaderText = "Sale Date";

            // Adjust column visibility as requested.
            // (Assumes the following order:)
            // [0] Customer Name (visible)
            // [1] Product Name (visible)
            // [2] Category Name (visible)
            // [3,4,5] IDs -> hide these columns.
            // [6] Quantity Sold (visible)
            // [7] Sales Price (visible)
            // [8] Sale Date (visible)
            // [9] to [15] -> hide these columns.
            if (dgvSales.Columns.Count >= 16)
            {
                dgvSales.Columns[0].Visible = true;
                dgvSales.Columns[1].Visible = true;
                dgvSales.Columns[2].Visible = true;
                dgvSales.Columns[3].Visible = false;
                dgvSales.Columns[4].Visible = false;
                dgvSales.Columns[5].Visible = false;
                dgvSales.Columns[6].Visible = true;
                dgvSales.Columns[7].Visible = true;
                dgvSales.Columns[8].Visible = true;
                for (int i = 9; i <= 17 && i < dgvSales.Columns.Count; i++)
                {
                    dgvSales.Columns[i].Visible = false;
                }
            }
        }

        /// <summary>
        /// Loads existing return records into dgvReturns.
        /// </summary>
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

            dgvReturns.Columns[0].Visible = false;
            dgvReturns.Columns[1].Visible = false;
            dgvReturns.Columns[2].Visible = false;
            dgvReturns.Columns[3].Visible = false;
            dgvReturns.Columns[10].Visible = false;
        }

        /// <summary>
        /// Clears input fields.
        /// </summary>
        private void ClearInputs()
        {
            txtSaleID.Text = "";
            txtProductName.Text = "";
            txtCustomerName.Text = "";
            nudReturnQuantity.Value = 0;
            dtpReturnDate.Value = DateTime.Today;
            txtReturnReason.Text = "";
            currentReturn = new ReturnDetailDTO();
            selectedSale = new SalesDetailDTO();
        }

        // When a sale is selected, capture its details.
        private void dgvSales_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            try
            {
                selectedSale = new SalesDetailDTO
                {
                    SalesID = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["SalesID"].Value),
                    ProductID = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["ProductID"].Value),
                    CustomerID = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["CustomerID"].Value),
                    ProductName = dgvSales.Rows[e.RowIndex].Cells["ProductName"].Value.ToString(),
                    CustomerName = dgvSales.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString(),
                    SalesAmount = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["SalesAmount"].Value)
                };
                txtSaleID.Text = selectedSale.SalesID.ToString();
                txtProductName.Text = selectedSale.ProductName;
                txtCustomerName.Text = selectedSale.CustomerName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting sale: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When a return record is selected, populate the detail fields for editing.
        private void dgvReturns_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            try
            {
                currentReturn = new ReturnDetailDTO
                {
                    ReturnID = Convert.ToInt32(dgvReturns.Rows[e.RowIndex].Cells["ReturnID"].Value),
                    SalesID = Convert.ToInt32(dgvReturns.Rows[e.RowIndex].Cells["SalesID"].Value),
                    ProductID = Convert.ToInt32(dgvReturns.Rows[e.RowIndex].Cells["ProductID"].Value),
                    CustomerID = Convert.ToInt32(dgvReturns.Rows[e.RowIndex].Cells["CustomerID"].Value),
                    ReturnQuantity = Convert.ToInt32(dgvReturns.Rows[e.RowIndex].Cells["ReturnQuantity"].Value),
                    ReturnDate = Convert.ToDateTime(dgvReturns.Rows[e.RowIndex].Cells["ReturnDate"].Value),
                    ReturnReason = dgvReturns.Rows[e.RowIndex].Cells["ReturnReason"].Value.ToString(),
                    ProductName = dgvReturns.Rows[e.RowIndex].Cells["ProductName"].Value.ToString(),
                    CustomerName = dgvReturns.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString()
                };

                txtSaleID.Text = currentReturn.SalesID.ToString();
                txtProductName.Text = currentReturn.ProductName;
                txtCustomerName.Text = currentReturn.CustomerName;
                nudReturnQuantity.Value = currentReturn.ReturnQuantity;
                dtpReturnDate.Value = currentReturn.ReturnDate;
                txtReturnReason.Text = currentReturn.ReturnReason;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting return record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Insert a new return record.
        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSaleID.Text))
                {
                    MessageBox.Show("Please select a sale record first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nudReturnQuantity.Value <= 0)
                {
                    MessageBox.Show("Return quantity must be greater than zero.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nudReturnQuantity.Value > selectedSale.SalesAmount)
                {
                    MessageBox.Show("Return quantity exceeds quantity sold.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ReturnDetailDTO newReturn = new ReturnDetailDTO
                {
                    SalesID = selectedSale.SalesID,
                    ProductID = selectedSale.ProductID,
                    CustomerID = selectedSale.CustomerID,
                    ReturnQuantity = Convert.ToInt32(nudReturnQuantity.Value),
                    ReturnDate = dtpReturnDate.Value.Date,
                    ReturnReason = txtReturnReason.Text.Trim()
                };

                if (returnBLL.Insert(newReturn))
                {
                    MessageBox.Show("Return processed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReturnData();
                    ClearInputs();
                    LoadSalesData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting return: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update an existing return record.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReturn.ReturnID == 0)
                {
                    MessageBox.Show("Please select a return record to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentReturn.ReturnQuantity = Convert.ToInt32(nudReturnQuantity.Value);
                currentReturn.ReturnDate = dtpReturnDate.Value.Date;
                currentReturn.ReturnReason = txtReturnReason.Text.Trim();

                if (returnBLL.Update(currentReturn))
                {
                    MessageBox.Show("Return updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReturnData();
                    ClearInputs();
                    LoadSalesData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating return: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete the selected return record.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReturn.ReturnID == 0)
                {
                    MessageBox.Show("Please select a return record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to delete this return record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (returnBLL.Delete(currentReturn))
                    {
                        MessageBox.Show("Return record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReturnData();
                        ClearInputs();
                        LoadSalesData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting return: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clear input fields.
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
