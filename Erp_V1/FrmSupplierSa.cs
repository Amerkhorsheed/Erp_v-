//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;
//using System;
//using System.Linq;
//using System.Windows.Forms;

//namespace Erp_V1
//{
//    public partial class FrmSupplierSa : Form
//    {
//        public FrmSupplierSa()
//        {
//            InitializeComponent();
//            // Wire up events.
//            txtSupplierSearch.TextChanged += txtSupplierSearch_TextChanged;
//            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
//        }

//        // Business objects.
//        public SupplierBLL bll = new SupplierBLL();
//        public SupplierDTO dto = new SupplierDTO();
//        public SupplierDetailDTO detail = new SupplierDetailDTO();
//        public bool isUpdate = false;
//        bool combofull = false;

//        private void FrmSupplierSa_Load(object sender, EventArgs e)
//        {
//            try
//            {
//                dto = bll.Select();

//                // Setup category combo box.
//                // Set DisplayMember and ValueMember before assigning DataSource.
//                cmbCategory.DisplayMember = "CategoryName";
//                cmbCategory.ValueMember = "ID";
//                cmbCategory.DataSource = dto.Categories;
//                cmbCategory.SelectedIndex = -1;

//                if (!isUpdate)
//                {
//                    // Setup product grid.
//                    gridProduct.DataSource = dto.Products;
//                    gridProduct.Columns["ProductName"].HeaderText = "Product Name";
//                    gridProduct.Columns["CategoryName"].HeaderText = "Category Name";
//                    gridProduct.Columns["stockAmount"].HeaderText = "Stock Amount";
//                    gridProduct.Columns["price"].Visible = false;
//                    gridProduct.Columns["ProductID"].Visible = false;
//                    gridProduct.Columns["CategoryID"].Visible = false;
//                    gridProduct.Columns["isCategoryDeleted"].Visible = false;
//                    gridProduct.Columns["Sale_Price"].HeaderText = "Sales Price";
//                    gridProduct.Columns["MinQty"].HeaderText = "Min Supply Qty";
//                    gridProduct.Columns["MaxDiscount"].HeaderText = "Max Discount";

//                    // Setup supplier grid with manual column binding.
//                    gridSupplier.AutoGenerateColumns = false;
//                    gridSupplier.Columns.Clear();

//                    // Supplier ID (hidden).
//                    DataGridViewTextBoxColumn colID = new DataGridViewTextBoxColumn
//                    {
//                        Name = "SupplierID",
//                        DataPropertyName = "SupplierID",
//                        HeaderText = "Supplier ID",
//                        Visible = false
//                    };
//                    gridSupplier.Columns.Add(colID);

//                    // Supplier Name.
//                    DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
//                    {
//                        Name = "SupplierName",
//                        DataPropertyName = "SupplierName",
//                        HeaderText = "Supplier Name"
//                    };
//                    gridSupplier.Columns.Add(colName);

//                    // Phone Number.
//                    DataGridViewTextBoxColumn colPhone = new DataGridViewTextBoxColumn
//                    {
//                        Name = "PhoneNumber",
//                        DataPropertyName = "PhoneNumber",
//                        HeaderText = "Phone Number"
//                    };
//                    gridSupplier.Columns.Add(colPhone);

//                    // Bind supplier grid to a distinct list: group by SupplierName and PhoneNumber.
//                    var distinctSuppliers = dto.Suppliers
//                        .GroupBy(s => new { s.SupplierName, s.PhoneNumber })
//                        .Select(g => new SupplierDetailDTO
//                        {
//                            SupplierID = g.First().SupplierID,
//                            SupplierName = g.Key.SupplierName,
//                            PhoneNumber = g.Key.PhoneNumber
//                        })
//                        .ToList();
//                    gridSupplier.DataSource = distinctSuppliers;

//                    if (dto.Categories.Count > 0)
//                        combofull = true;
//                }
//                else
//                {
//                    // Update mode: hide selection panel and load existing details.
//                    panel1.Hide();
//                    txtSupplierName.Text = detail.SupplierName;
//                    txtProductName.Text = detail.ProductName;
//                    txtPrice.Text = detail.Price.ToString();
//                    txtProductSalesAmount.Text = detail.SuppliedAmount.ToString();
//                    txtDiscount.Text = detail.Discount.ToString();

//                    // Retrieve latest product details.
//                    var product = dto.Products.FirstOrDefault(x => x.ProductID == detail.ProductID);
//                    if (product != null)
//                    {
//                        detail.StockAmount = product.stockAmount;
//                        detail.MaxDiscount = product.MaxDiscount;
//                        txtStock.Text = detail.StockAmount.ToString();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error loading supplier data: " + ex.Message,
//                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void txtProductSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            e.Handled = General.isNumber(e);
//        }

//        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            e.Handled = General.isNumber(e);
//        }

//        // Load product details when a product row is selected.
//        private void gridProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
//        {
//            try
//            {
//                if (e.RowIndex < 0 || e.RowIndex >= gridProduct.Rows.Count)
//                    return;

//                var row = gridProduct.Rows[e.RowIndex];
//                var product = row.DataBoundItem as ProductDetailDTO;
//                if (product != null)
//                {
//                    detail.CategoryID = product.CategoryID;
//                    detail.ProductID = product.ProductID;
//                    detail.ProductName = product.ProductName;
//                    detail.StockAmount = product.stockAmount;
//                    detail.Price = (int)product.Sale_Price;
//                    detail.MaxDiscount = product.MaxDiscount;

//                    txtProductName.Text = detail.ProductName;
//                    txtPrice.Text = detail.Price.ToString();
//                    txtStock.Text = detail.StockAmount.ToString();
//                }
//                else
//                {
//                    MessageBox.Show("Selected product data is invalid.",
//                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error selecting product: " + ex.Message,
//                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        // Load supplier details when a supplier row is selected.
//        private void gridSupplier_RowEnter(object sender, DataGridViewCellEventArgs e)
//        {
//            try
//            {
//                if (e.RowIndex < 0 || e.RowIndex >= gridSupplier.Rows.Count)
//                    return;

//                var supplierRow = gridSupplier.Rows[e.RowIndex];

//                // Get supplier name.
//                var supplierNameObj = supplierRow.Cells["SupplierName"].Value;
//                if (supplierNameObj == null)
//                {
//                    MessageBox.Show("Supplier name is missing.",
//                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                detail.SupplierName = supplierNameObj.ToString();

//                // Get supplier ID.
//                var supplierIDObj = supplierRow.Cells["SupplierID"].Value;
//                if (supplierIDObj == null || !int.TryParse(supplierIDObj.ToString(), out int supplierId))
//                {
//                    MessageBox.Show("Supplier ID is missing or invalid.",
//                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }
//                detail.SupplierID = supplierId;
//                txtSupplierName.Text = detail.SupplierName;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error selecting supplier: " + ex.Message,
//                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        // Update the stock textbox when the supplied amount changes.
//        // Always calculate new stock as original product stock + supplied quantity.
//        private void txtProductSalesAmount_TextChanged(object sender, EventArgs e)
//        {
//            if (int.TryParse(txtProductSalesAmount.Text, out int supplyQty))
//            {
//                int newStock = detail.StockAmount + supplyQty;
//                txtStock.Text = newStock.ToString();
//            }
//            else
//            {
//                txtStock.Text = detail.StockAmount.ToString();
//            }
//        }

//        // Filter suppliers by name.
//        private void txtSupplierSearch_TextChanged(object sender, EventArgs e)
//        {
//            string searchText = txtSupplierSearch.Text.Trim().ToLower();
//            var filteredSuppliers = dto.Suppliers
//                .Where(s => !string.IsNullOrEmpty(s.SupplierName) &&
//                            s.SupplierName.ToLower().Contains(searchText))
//                .GroupBy(s => new { s.SupplierName, s.PhoneNumber })
//                .Select(g => new SupplierDetailDTO
//                {
//                    SupplierID = g.First().SupplierID,
//                    SupplierName = g.Key.SupplierName,
//                    PhoneNumber = g.Key.PhoneNumber
//                })
//                .ToList();
//            gridSupplier.DataSource = filteredSuppliers;
//        }

//        // Filter products by category when a category is selected.
//        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (cmbCategory.SelectedIndex != -1 && cmbCategory.SelectedItem is CategoryDetailDTO selectedCategory)
//            {
//                int categoryId = selectedCategory.ID;
//                gridProduct.DataSource = dto.Products
//                    .Where(p => p.CategoryID == categoryId)
//                    .ToList();
//            }
//            else
//            {
//                gridProduct.DataSource = dto.Products;
//            }
//        }

//        // Save the supplier record and update product stock.
//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(txtProductSalesAmount.Text) ||
//                    !int.TryParse(txtProductSalesAmount.Text, out int supplyQty))
//                {
//                    MessageBox.Show("Please enter a valid supplied amount",
//                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                float enteredDiscount = 0;
//                if (!string.IsNullOrWhiteSpace(txtDiscount.Text) &&
//                    !float.TryParse(txtDiscount.Text, out enteredDiscount))
//                {
//                    MessageBox.Show("Invalid discount value. Please enter a valid number.",
//                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (enteredDiscount > detail.MaxDiscount)
//                {
//                    MessageBox.Show("Discount cannot exceed " + detail.MaxDiscount,
//                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                // Always calculate the new stock as original stock + supplied quantity.
//                int updatedStock = detail.StockAmount + supplyQty;

//                if (!isUpdate)
//                {
//                    if (detail.ProductID == 0)
//                    {
//                        MessageBox.Show("Please select a product from the product table",
//                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }
//                    if (detail.SupplierID == 0)
//                    {
//                        MessageBox.Show("Please select a supplier from the supplier table",
//                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    detail.SuppliedAmount = supplyQty;
//                    detail.Discount = enteredDiscount;
//                    if (!int.TryParse(txtPrice.Text, out int price))
//                    {
//                        MessageBox.Show("Invalid price value.",
//                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }
//                    detail.Price = price;

//                    // Update product stock and then insert the supplier record.
//                    if (bll.UpdateProductStock(detail.ProductID, updatedStock))
//                    {
//                        detail.CurrentStock = updatedStock;
//                        if (bll.Insert(detail))
//                        {
//                            MessageBox.Show("Supplier record was added successfully",
//                                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            // Refresh data.
//                            bll = new SupplierBLL();
//                            dto = bll.Select();
//                            gridProduct.DataSource = dto.Products;

//                            // Rebind distinct supplier list.
//                            var distinctSuppliers = dto.Suppliers
//                                .GroupBy(s => new { s.SupplierName, s.PhoneNumber })
//                                .Select(g => new SupplierDetailDTO
//                                {
//                                    SupplierID = g.First().SupplierID,
//                                    SupplierName = g.Key.SupplierName,
//                                    PhoneNumber = g.Key.PhoneNumber
//                                })
//                                .ToList();
//                            gridSupplier.DataSource = distinctSuppliers;

//                            cmbCategory.DataSource = dto.Categories;
//                            txtProductSalesAmount.Clear();
//                            txtDiscount.Clear();
//                        }
//                    }
//                    else
//                    {
//                        MessageBox.Show("Failed to update product stock",
//                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//                else
//                {
//                    bool changesMade = false;
//                    if (!int.TryParse(txtProductSalesAmount.Text, out int newSupplyQty) ||
//                        !int.TryParse(txtPrice.Text, out int newPrice))
//                    {
//                        MessageBox.Show("Invalid input values.",
//                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }
//                    float newDiscount = enteredDiscount;

//                    if (detail.SuppliedAmount != newSupplyQty)
//                        changesMade = true;
//                    if (detail.Price != newPrice)
//                        changesMade = true;
//                    if (detail.Discount != newDiscount)
//                        changesMade = true;

//                    if (!changesMade)
//                    {
//                        MessageBox.Show("There is no change",
//                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                        return;
//                    }

//                    // Recalculate new stock.
//                    updatedStock = detail.StockAmount + newSupplyQty;

//                    // Update product stock and then update the supplier record.
//                    if (bll.UpdateProductStock(detail.ProductID, updatedStock))
//                    {
//                        detail.CurrentStock = updatedStock;
//                        detail.SuppliedAmount = newSupplyQty;
//                        detail.Price = newPrice;
//                        detail.Discount = newDiscount;

//                        if (bll.Update(detail))
//                        {
//                            MessageBox.Show("Supplier record was updated successfully",
//                                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            this.Close();
//                        }
//                        else
//                        {
//                            MessageBox.Show("Failed to update supplier record",
//                                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                    }
//                    else
//                    {
//                        MessageBox.Show("Failed to update product stock",
//                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error saving supplier data: " + ex.Message,
//                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}
