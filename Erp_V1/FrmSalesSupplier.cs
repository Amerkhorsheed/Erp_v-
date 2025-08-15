using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmSalesSupplier : Form
    {
        public FrmSalesSupplier()
        {
            InitializeComponent();
        }

        // Business logic and data objects.
        public SalesBLL bll = new SalesBLL();
        public SalesDTO dto = new SalesDTO();
        // This object is used for holding both sales details and selected product/customer info.
        public SalesDetailDTO detail = new SalesDetailDTO();
        public bool isUpdate = false;
        bool combofull = false;

        private void FrmSales_Load(object sender, EventArgs e)
        {
            try
            {
                dto = bll.Select();

                // Setup category combo box.
                cmbCategory.DataSource = dto.Categories;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "ID";
                cmbCategory.SelectedIndex = -1;

                if (!isUpdate)
                {
                    // Setup product grid.
                    gridProduct.DataSource = dto.Products;
                    gridProduct.Columns["ProductName"].HeaderText = "Product Name";
                    gridProduct.Columns["CategoryName"].HeaderText = "Category Name";
                    gridProduct.Columns["stockAmount"].HeaderText = "Stock Amount";
                    gridProduct.Columns["price"].Visible = false;
                    gridProduct.Columns["ProductID"].Visible = false;
                    gridProduct.Columns["CategoryID"].Visible = false;
                    gridProduct.Columns["isCategoryDeleted"].Visible = false;
                    gridProduct.Columns["Sale_Price"].HeaderText = "Sales Price";
                    gridProduct.Columns["MinQty"].HeaderText = "Max Sale Qty";
                    gridProduct.Columns["MaxDiscount"].HeaderText = "Max Discount";

                    // Setup customer grid.
                    gridSupplier.DataSource = dto.Customers;
                    if (gridSupplier.Columns.Count > 0)
                        gridSupplier.Columns[0].Visible = false;
                    if (gridSupplier.Columns.Count > 1)
                        gridSupplier.Columns[1].HeaderText = "Customer Name";
                    if (gridSupplier.Columns.Count > 2)
                        gridSupplier.Columns[2].Visible = false;
                    if (gridSupplier.Columns.Count > 3)
                        gridSupplier.Columns[3].HeaderText = "Customer Phone";
                    if (gridSupplier.Columns.Count > 4)
                        gridSupplier.Columns[4].Visible = false;

                    if (dto.Categories.Count > 0)
                        combofull = true;
                }
                else
                {
                    // Update mode: hide the product/customer panel.
                    panel1.Hide();
                    txtSupplierName.Text = detail.CustomerName;
                    txtProductName.Text = detail.ProductName;
                    txtPrice.Text = detail.Price.ToString();
                    txtProductSalesAmount.Text = detail.SalesAmount.ToString();
                    // Display the current applied discount, not the max discount.
                    txtDiscount.Text = detail.MaxDiscount.ToString();

                    // Retrieve the latest product details.
                    var product = dto.Products.FirstOrDefault(x => x.ProductID == detail.ProductID);
                    if (product != null)
                    {
                        detail.StockAmount = product.stockAmount;
                        detail.MinQty = product.MinQty;
                        detail.MaxDiscount = product.MaxDiscount;
                        txtStock.Text = detail.StockAmount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtProductSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        // When a product row is selected, retrieve the full product details.
        // Note: The grid's data source is assumed to be a list of ProductDetailDTO objects.
        private void gridProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= gridProduct.Rows.Count)
                    return;

                var row = gridProduct.Rows[e.RowIndex];
                var product = row.DataBoundItem as ProductDetailDTO;
                if (product != null)
                {
                    // ADD THIS LINE TO CAPTURE CATEGORYID
                    detail.CategoryID = product.CategoryID;

                    detail.ProductID = product.ProductID;
                    detail.ProductName = product.ProductName;
                    detail.StockAmount = product.stockAmount;
                    detail.Price = (int)product.Sale_Price;
                    detail.MinQty = product.MinQty;
                    detail.MaxDiscount = product.MaxDiscount;

                    txtProductName.Text = detail.ProductName;
                    txtPrice.Text = detail.Price.ToString();
                    txtStock.Text = detail.StockAmount.ToString();
                }
                else
                {
                    MessageBox.Show("Selected product data is invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Improved event handler with robust null and bounds checking for customer grid.
        private void gridCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= gridSupplier.Rows.Count)
                    return;

                var customerRow = gridSupplier.Rows[e.RowIndex];

                // Ensure that the customer row has at least two cells.
                if (customerRow.Cells.Count < 2)
                {
                    MessageBox.Show("Customer data is incomplete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Retrieve and validate the customer name.
                var customerNameObj = customerRow.Cells[1].Value;
                if (customerNameObj == null)
                {
                    MessageBox.Show("Customer name is missing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                detail.CustomerName = customerNameObj.ToString();

                // Retrieve and validate the customer ID.
                var customerIDObj = customerRow.Cells[0].Value;
                if (customerIDObj == null || !int.TryParse(customerIDObj.ToString(), out int customerId))
                {
                    MessageBox.Show("Customer ID is missing or invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                detail.CustomerID = customerId;
                txtSupplierName.Text = detail.CustomerName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate sales amount input.
                if (string.IsNullOrWhiteSpace(txtProductSalesAmount.Text) ||
                    !int.TryParse(txtProductSalesAmount.Text, out int saleQty))
                {
                    MessageBox.Show("Please enter a valid sales amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse the discount value.
                float enteredDiscount = 0;
                if (!string.IsNullOrWhiteSpace(txtDiscount.Text) &&
                    !float.TryParse(txtDiscount.Text, out enteredDiscount))
                {
                    MessageBox.Show("Invalid discount value. Please enter a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate discount and quantity constraints.
                if (enteredDiscount > detail.MaxDiscount)
                {
                    MessageBox.Show("Discount cannot exceed " + detail.MaxDiscount, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (saleQty > detail.MinQty)
                {
                    MessageBox.Show("Sales quantity cannot exceed " + detail.MinQty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!isUpdate)
                {
                    // Insert branch.
                    if (detail.ProductID == 0)
                    {
                        MessageBox.Show("Please select a product from the product table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (detail.CustomerID == 0)
                    {
                        MessageBox.Show("Please select a customer from the customer table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (detail.StockAmount < saleQty)
                    {
                        MessageBox.Show("You do not have enough products for sale", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Set sales details.
                    detail.SalesAmount = saleQty;
                    detail.MaxDiscount = enteredDiscount;
                    if (!int.TryParse(txtPrice.Text, out int price))
                    {
                        MessageBox.Show("Invalid price value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    detail.Price = price;
                    detail.SalesDate = DateTime.Today;

                    if (bll.Insert(detail))
                    {
                        MessageBox.Show("Sales was added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh data after insert.
                        bll = new SalesBLL();
                        dto = bll.Select();
                        gridProduct.DataSource = dto.Products;
                        gridSupplier.DataSource = dto.Customers;
                        cmbCategory.DataSource = dto.Categories;
                        txtProductSalesAmount.Clear();
                        txtDiscount.Clear();
                    }
                }
                else
                {
                    // Update branch: Check for changes.
                    bool changesMade = false;
                    if (!int.TryParse(txtProductSalesAmount.Text, out int newSalesAmount) ||
                        !int.TryParse(txtPrice.Text, out int newPrice))
                    {
                        MessageBox.Show("Invalid input values.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    float newDiscount = enteredDiscount;

                    if (detail.SalesAmount != newSalesAmount)
                        changesMade = true;
                    if (detail.Price != newPrice)
                        changesMade = true;
                    if (detail.MaxDiscount != newDiscount)
                        changesMade = true;

                    if (!changesMade)
                    {
                        MessageBox.Show("There is no change", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validate constraints.
                    if (newDiscount > detail.MaxDiscount)
                    {
                        MessageBox.Show("Discount cannot exceed " + detail.MaxDiscount, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (newSalesAmount > detail.MinQty)
                    {
                        MessageBox.Show("Sales quantity cannot exceed " + detail.MinQty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Recalculate available stock.
                    int availableStock = detail.StockAmount + detail.SalesAmount;
                    if (availableStock < newSalesAmount)
                    {
                        MessageBox.Show("You do not have enough products for sale", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Update sales details.
                    detail.SalesAmount = newSalesAmount;
                    detail.Price = newPrice;
                    detail.MaxDiscount = newDiscount;
                    detail.StockAmount = availableStock - newSalesAmount;
                    detail.SalesDate = DateTime.Today;

                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Sales were updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update sales", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving sales data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
