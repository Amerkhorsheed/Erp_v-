using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class frmPurchase : Form
    {
        public frmPurchase()
        {
            InitializeComponent();
        }

        // Business logic and data objects.
        public PurchasesBLL bll = new PurchasesBLL();
        public PurchasesDTO dto = new PurchasesDTO();
        // This object holds both purchase details and the selected product/supplier information.
        public PurchasesDetailDTO detail = new PurchasesDetailDTO();
        public bool isUpdate = false;
        bool combofull = false;

        private void frmPurchase_Load_1(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the DTO from the business layer.
                dto = bll.Select();

                // Ensure the lists are not null.
                if (dto.Categories == null)
                    dto.Categories = new List<CategoryDetailDTO>();
                if (dto.Products == null)
                    dto.Products = new List<ProductDetailDTO>();
                if (dto.Suppliers == null)
                    dto.Suppliers = new List<SupplierDetailDTO>();

                // Setup the category combo box.
                cmbCategory.DataSource = dto.Categories;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "ID";
                cmbCategory.SelectedIndex = -1;

                if (!isUpdate)
                {
                    // Ensure the DataGridViews auto-generate columns.
                    gridProduct.AutoGenerateColumns = true;
                    gridSupplier.AutoGenerateColumns = true;

                    // Bind the product grid.
                    gridProduct.DataSource = dto.Products;
                    gridProduct.Refresh();

                    // Bind the supplier grid.
                    gridSupplier.DataSource = dto.Suppliers;
                    gridSupplier.Refresh();

                    // Optionally adjust grid columns after binding if they exist.
                    if (gridProduct.Columns["ProductName"] != null)
                        gridProduct.Columns["ProductName"].HeaderText = "Product Name";
                    if (gridProduct.Columns["CategoryName"] != null)
                        gridProduct.Columns["CategoryName"].HeaderText = "Category Name";
                    if (gridProduct.Columns["stockAmount"] != null)
                        gridProduct.Columns["stockAmount"].HeaderText = "Stock Amount";
                    if (gridProduct.Columns["price"] != null)
                        gridProduct.Columns["price"].HeaderText = "Price";
                    if (gridProduct.Columns["ProductID"] != null)
                        gridProduct.Columns["ProductID"].Visible = false;
                    if (gridProduct.Columns["CategoryID"] != null)
                        gridProduct.Columns["CategoryID"].Visible = false;
                    if (gridProduct.Columns["isCategoryDeleted"] != null)
                        gridProduct.Columns["isCategoryDeleted"].Visible = false;
                    if (gridProduct.Columns["Sale_Price"] != null)
                        gridProduct.Columns["Sale_Price"].Visible = false;
                    if (gridProduct.Columns["MinQty"] != null)
                        gridProduct.Columns["MinQty"].Visible = false;
                    if (gridProduct.Columns["MaxDiscount"] != null)
                        gridProduct.Columns["MaxDiscount"].Visible = false;

                    // Adjust supplier grid columns.
                    // Note: The order and index depend on how your supplier DTO is defined.
                    if (gridSupplier.Columns.Count > 0)
                        gridSupplier.Columns[0].Visible = false;
                    if (gridSupplier.Columns.Count > 1)
                        gridSupplier.Columns[1].HeaderText = "Supplier Name";
                    if (gridSupplier.Columns.Count > 2)
                        gridSupplier.Columns[2].HeaderText = "Supplier Phone";
                    if (gridSupplier.Columns.Count > 3)
                        gridSupplier.Columns[3].Visible = false;
                    if (gridSupplier.Columns.Count > 4)
                        gridSupplier.Columns[4].Visible = false;

                    combofull = dto.Categories.Count > 0;
                }
                else
                {
                    // Update mode: hide the selection panel.
                    panel1.Hide();
                    txtSupplierName.Text = detail.SupplierName;
                    txtProductName.Text = detail.ProductName;
                    txtPrice.Text = detail.Price.ToString();
                    txtPurchaseAmount.Text = detail.PurchaseAmount.ToString();

                    // Refresh product data.
                    var product = dto.Products.FirstOrDefault(x => x.ProductID == detail.ProductID);
                    if (product != null)
                    {
                        detail.StockAmount = product.stockAmount;
                        txtStock.Text = detail.StockAmount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading purchase data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPurchaseAmount_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        // When a product row is selected, capture its full details.
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
                    // Capture necessary product information.
                    detail.CategoryID = product.CategoryID;
                    detail.ProductID = product.ProductID;
                    detail.ProductName = product.ProductName;
                    detail.StockAmount = product.stockAmount;
                    // Use the product's Price (not the sale price) for purchases.
                    detail.Price = product.price;

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

        // When a supplier row is selected, capture its details.
        private void gridSupplier_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= gridSupplier.Rows.Count)
                    return;

                var supplierRow = gridSupplier.Rows[e.RowIndex];

                if (supplierRow.Cells.Count < 2)
                {
                    MessageBox.Show("Supplier data is incomplete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var supplierNameObj = supplierRow.Cells[1].Value;
                if (supplierNameObj == null)
                {
                    MessageBox.Show("Supplier name is missing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                detail.SupplierName = supplierNameObj.ToString();

                var supplierIDObj = supplierRow.Cells[0].Value;
                if (supplierIDObj == null || !int.TryParse(supplierIDObj.ToString(), out int supplierId))
                {
                    MessageBox.Show("Supplier ID is missing or invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                detail.SupplierID = supplierId;
                txtSupplierName.Text = detail.SupplierName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate purchase amount input.
                if (string.IsNullOrWhiteSpace(txtPurchaseAmount.Text) ||
                    !int.TryParse(txtPurchaseAmount.Text, out int purchaseQty))
                {
                    MessageBox.Show("Please enter a valid purchase amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if (detail.SupplierID == 0)
                    {
                        MessageBox.Show("Please select a supplier from the supplier table", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Set purchase details.
                    detail.PurchaseAmount = purchaseQty;
                    if (!int.TryParse(txtPrice.Text, out int price))
                    {
                        MessageBox.Show("Invalid price value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    detail.Price = price;
                    detail.PurchaseDate = DateTime.Today;

                    // Business logic: the purchase quantity is added to the product’s current sales amount.
                    if (bll.Insert(detail))
                    {
                        MessageBox.Show("Purchase was added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh data after insert.
                        bll = new PurchasesBLL();
                        dto = bll.Select();
                        gridProduct.DataSource = dto.Products;
                        gridProduct.Refresh();
                        gridSupplier.DataSource = dto.Suppliers;
                        gridSupplier.Refresh();
                        cmbCategory.DataSource = dto.Categories;
                        txtPurchaseAmount.Clear();
                    }
                }
                else
                {
                    // Update branch.
                    bool changesMade = false;
                    if (!int.TryParse(txtPurchaseAmount.Text, out int newPurchaseAmount) ||
                        !int.TryParse(txtPrice.Text, out int newPrice))
                    {
                        MessageBox.Show("Invalid input values.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (detail.PurchaseAmount != newPurchaseAmount)
                        changesMade = true;
                    if (detail.Price != newPrice)
                        changesMade = true;

                    if (!changesMade)
                    {
                        MessageBox.Show("There is no change", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Adjust stock by removing the old purchase quantity and then adding the new one.
                    int availableStock = detail.StockAmount - detail.PurchaseAmount + newPurchaseAmount;
                    detail.PurchaseAmount = newPurchaseAmount;
                    detail.Price = newPrice;
                    detail.StockAmount = availableStock;
                    detail.PurchaseDate = DateTime.Today;

                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Purchase was updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update purchase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving purchase data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
    }
}
