using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmPurchaseList : Form
    {
        public FrmPurchaseList()
        {
            InitializeComponent();
        }

        // Business logic and data objects for purchases.
        PurchasesBLL bll = new PurchasesBLL();
        PurchasesDTO dto = new PurchasesDTO();
        PurchasesDetailDTO detail = new PurchasesDetailDTO();

        private void FrmPurchaseList_Load_1(object sender, EventArgs e)
        {
            // Load purchase data from BLL.
            dto = bll.Select();
            dataGridView1.DataSource = dto.Purchases;

            // Set column headers (adjust indices based on your DTO's property order).
            dataGridView1.Columns[0].HeaderText = "Supplier Name";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[6].HeaderText = "Purchase Amount";
            dataGridView1.Columns[7].HeaderText = "Price";
            dataGridView1.Columns[8].HeaderText = "Purchase Date";

            // Hide unnecessary columns.
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;

            // Setup the category ComboBox.
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtPurchaseAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open the purchase form for adding a new purchase.
            frmPurchase frm = new frmPurchase();
            frm.dto = dto;
            frm.ShowDialog();

            // Refresh purchase data.
            dto = bll.Select();
            dataGridView1.DataSource = dto.Purchases;
            CleanFilters();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PurchasesDetailDTO> filteredList = dto.Purchases;

            // Filter by Product Name.
            if (!string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                filteredList = filteredList.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            }

            // Filter by Supplier Name.
            if (!string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                filteredList = filteredList.Where(x => x.SupplierName.Contains(txtSupplierName.Text)).ToList();
            }

            // Filter by Category.
            if (cmbCategory.SelectedIndex != -1)
            {
                int selectedCategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
                filteredList = filteredList.Where(x => x.CategoryID == selectedCategoryId).ToList();
            }

            // Filter by Price.
            if (!string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                if (int.TryParse(txtPrice.Text, out int price))
                {
                    if (rbPriceEquals.Checked)
                        filteredList = filteredList.Where(x => x.Price == price).ToList();
                    else if (rbPriceMore.Checked)
                        filteredList = filteredList.Where(x => x.Price > price).ToList();
                    else if (rbPriceLess.Checked)
                        filteredList = filteredList.Where(x => x.Price < price).ToList();
                    else
                        MessageBox.Show("Please select a criterion from the price group.");
                }
                else
                {
                    MessageBox.Show("Invalid price format.");
                }
            }

            // Filter by Purchase Amount.
            if (!string.IsNullOrWhiteSpace(txtSalesAmount.Text))
            {
                if (int.TryParse(txtSalesAmount.Text, out int purchaseAmount))
                {
                    if (rbSalesEqual.Checked)
                        filteredList = filteredList.Where(x => x.PurchaseAmount == purchaseAmount).ToList();
                    else if (rbPriceMore.Checked)
                        filteredList = filteredList.Where(x => x.PurchaseAmount > purchaseAmount).ToList();
                    else if (rbSalesLess.Checked)
                        filteredList = filteredList.Where(x => x.PurchaseAmount < purchaseAmount).ToList();
                    else
                        MessageBox.Show("Please select a criterion from the purchase amount group.");
                }
                else
                {
                    MessageBox.Show("Invalid purchase amount format.");
                }
            }

            // Filter by Date Range.
            if (chDate.Checked)
            {
                DateTime startDate = dpStart.Value.Date;
                DateTime endDate = dpEnd.Value.Date;
                filteredList = filteredList.Where(x => x.PurchaseDate >= startDate && x.PurchaseDate <= endDate).ToList();
            }

            // Update the DataGridView with the filtered list.
            dataGridView1.DataSource = filteredList;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void CleanFilters()
        {
            // Clear all filters.
            txtProductName.Text = string.Empty;
            txtSupplierName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtSalesAmount.Text = string.Empty;

            // Uncheck all radio buttons.
            rbPriceEquals.Checked = false;
            rbPriceMore.Checked = false;
            rbPriceLess.Checked = false;
            rbSalesEqual.Checked = false;
            rbSalesMore.Checked = false;
            rbSalesLess.Checked = false;

            // Reset date pickers.
            dpStart.Value = DateTime.Today;
            dpEnd.Value = DateTime.Today;

            // Reset the category ComboBox.
            cmbCategory.SelectedIndex = -1;

            // Reset the DataGridView data source.
            dataGridView1.DataSource = dto.Purchases;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Capture the selected purchase details.
            detail = new PurchasesDetailDTO();
            detail.PurchaseID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.SupplierName = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            detail.PurchaseAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.PurchaseID == 0)
            {
                MessageBox.Show("Please select a purchase from the table");
            }
            else
            {
                // Open the purchase form in update mode.
                frmPurchase frm = new frmPurchase
                {
                    isUpdate = true,
                    detail = detail,
                    dto = dto
                };

                this.Hide();
                frm.ShowDialog();
                this.Visible = true;

                // Refresh the purchase list.
                PurchasesBLL bll = new PurchasesBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Purchases;
                CleanFilters();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.PurchaseID == 0)
                MessageBox.Show("Please select a purchase from the table");
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "WARNING!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Purchase was deleted successfully");
                        bll = new PurchasesBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.Purchases;
                        CleanFilters();
                    }
                }
            }
        }

        private void txtSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

      
    }
}
