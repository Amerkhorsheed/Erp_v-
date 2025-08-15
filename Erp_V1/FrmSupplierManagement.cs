//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Windows.Forms;

//namespace Erp_V1
//{
//    public partial class FrmSupplierManagement : Form
//    {
//        #region Fields & Constructors

//        private readonly SupplierBLL _supplierBll = new SupplierBLL();
//        private SupplierDTO _supplierDto = new SupplierDTO();
//        private SupplierDetailDTO _currentSupplierDetail = new SupplierDetailDTO();
//        private bool _isUpdateMode = false;
//        private bool _isCategoryLoaded = false;

//        public FrmSupplierManagement()
//        {
//            InitializeComponent();
//        }

//        #endregion

//        #region Form Load & Initialization

//        private void FrmSupplierManagement_Load_1(object sender, EventArgs e)
//        {
//            try
//            {
//                ConfigureDataGridViews();
//                LoadInitialData();
//                InitializeFormControls();
//                RegisterEvents();
//            }
//            catch (Exception ex)
//            {
//                ShowErrorMessage($"Error initializing form: {ex.Message}");
//            }
//        }

//        private void ConfigureDataGridViews()
//        {
//            // Configure Product Grid
//            gridProduct.AutoGenerateColumns = false;
//            gridProduct.Columns.Clear();
//            gridProduct.Columns.AddRange(new DataGridViewColumn[]
//            {
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "ProductID",
//                    HeaderText = "Product ID",
//                    Visible = false
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "ProductName",
//                    HeaderText = "Product Name",
//                    Width = 150
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "CategoryName",
//                    HeaderText = "Category",
//                    Width = 120
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "stockAmount",
//                    HeaderText = "Current Stock",
//                    Width = 100
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "price",
//                    HeaderText = "Unit Price",
//                    Width = 100
//                }
//            });

//            // Configure Supplier Grid
//            gridSupplier.AutoGenerateColumns = false;
//            gridSupplier.Columns.Clear();
//            gridSupplier.Columns.AddRange(new DataGridViewColumn[]
//            {
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "SupplierID",
//                    HeaderText = "Supplier ID",
//                    Visible = false
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "SupplierName",
//                    HeaderText = "Supplier Name",
//                    Width = 150
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "PhoneNumber",
//                    HeaderText = "Contact",
//                    Width = 120
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "ProductName",
//                    HeaderText = "Product",
//                    Width = 150
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "SuppliedAmount",
//                    HeaderText = "Supplied Qty",
//                    Width = 100
//                },
//                new DataGridViewTextBoxColumn
//                {
//                    DataPropertyName = "Price",
//                    HeaderText = "Unit Price",
//                    Width = 100
//                }
//            });
//        }

//        private void LoadInitialData()
//        {
//            try
//            {
//                _supplierDto = _supplierBll.Select();

//                // Ensure data integrity
//                _supplierDto.Products = _supplierDto.Products ?? new List<ProductDetailDTO>();
//                _supplierDto.Suppliers = _supplierDto.Suppliers ?? new List<SupplierDetailDTO>();
//                _supplierDto.Categories = _supplierDto.Categories ?? new List<CategoryDetailDTO>();

//                // Bind data sources
//                cmbCategory.DataSource = _supplierDto.Categories;
//                gridProduct.DataSource = _supplierDto.Products;
//                gridSupplier.DataSource = _supplierDto.Suppliers;
//            }
//            catch (Exception ex)
//            {
//                ShowErrorMessage($"Data load failed: {ex.Message}");
//            }
//        }

//        private void InitializeFormControls()
//        {
//            if (!_isUpdateMode)
//            {
//                panelSelection.Visible = true;
//                panelInput.Left = 300; // Position input panel next to selection panel
//                ResetFormFields();
//            }
//            else
//            {
//                panelSelection.Visible = false;
//                panelInput.Left = 0; // Center input panel when in update mode
//                panelInput.Width = this.ClientSize.Width;
//                PopulateUpdateFields();
//            }

//            // Setup numeric field validation
//            txtPrice.KeyPress += txtNumeric_KeyPress;
//            txtSuppliedAmount.KeyPress += txtNumeric_KeyPress;
//            txtDiscount.KeyPress += txtNumeric_KeyPress;
//        }

//        private void RegisterEvents()
//        {
//            // Register grid events
//            gridProduct.RowEnter += gridProduct_RowEnter;
//            gridSupplier.RowEnter += gridSupplier_RowEnter;

//            // Register button events
//            btnSave.Click += btnSave_Click;
//            btnClose.Click += btnClose_Click;
//        }

//        private void PopulateUpdateFields()
//        {
//            txtSupplierName.Text = _currentSupplierDetail.SupplierName;
//            txtSupplierName.ReadOnly = true;

//            // Find product by ID
//            var product = _supplierDto.Products.FirstOrDefault(p => p.ProductID == _currentSupplierDetail.ProductID);
//            txtProductName.Text = product?.ProductName ?? "N/A";

//            txtPrice.Text = _currentSupplierDetail.Price.ToString();
//            txtSuppliedAmount.Text = _currentSupplierDetail.SuppliedAmount.ToString();
//            txtDiscount.Text = (_currentSupplierDetail.Discount * 100).ToString("F0") + "%";
//            txtCurrentStock.Text = product?.stockAmount.ToString() ?? "0";
//        }

//        #endregion

//        #region Event Handlers

//        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                if (cmbCategory.SelectedIndex > -1)
//                {
//                    int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
//                    FilterProductsByCategory(categoryId);
//                }
//                else
//                {
//                    // Reset filter - show all products
//                    gridProduct.DataSource = _supplierDto.Products;
//                }
//            }
//            catch (Exception ex)
//            {
//                ShowErrorMessage($"Error filtering products: {ex.Message}");
//            }
//        }

//        private void gridProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
//        {
//            if (IsValidGridSelection(gridProduct, e.RowIndex))
//            {
//                var product = gridProduct.Rows[e.RowIndex].DataBoundItem as ProductDetailDTO;
//                if (product != null)
//                {
//                    UpdateProductDetails(product);
//                }
//            }
//        }

//        private void gridSupplier_RowEnter(object sender, DataGridViewCellEventArgs e)
//        {
//            if (IsValidGridSelection(gridSupplier, e.RowIndex))
//            {
//                var supplier = gridSupplier.Rows[e.RowIndex].DataBoundItem as SupplierDetailDTO;
//                if (supplier != null)
//                {
//                    UpdateSupplierDetails(supplier);
//                }
//            }
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (ValidateSupplierInputs())
//            {
//                ProcessSupplierOperation();
//            }
//        }

//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            Close();
//        }

//        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            // Allow digits, control characters, and decimal point
//            bool isDecimalInput = sender == txtDiscount;

//            if (isDecimalInput)
//            {
//                // For discount field, allow decimal point
//                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '%';
//            }
//            else
//            {
//                // For other numeric fields, only allow digits and control characters
//                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
//            }
//        }

//        #endregion

//        #region Data Operations

//        private void FilterProductsByCategory(int categoryId)
//        {
//            if (categoryId > 0)
//            {
//                var filteredProducts = _supplierDto.Products.Where(p => p.CategoryID == categoryId).ToList();
//                gridProduct.DataSource = null;
//                gridProduct.DataSource = filteredProducts;
//            }
//        }

//        private void UpdateProductDetails(ProductDetailDTO product)
//        {
//            // Update the current supplier detail object with selected product info
//            _currentSupplierDetail.ProductID = product.ProductID;
//            _currentSupplierDetail.CurrentStock = product.stockAmount;
//            _currentSupplierDetail.Price = (int)product.price;

//            // Update UI fields
//            txtProductName.Text = product.ProductName;
//            txtPrice.Text = product.price.ToString();
//            txtCurrentStock.Text = product.stockAmount.ToString();

//            // Default the supplied amount if empty
//            if (string.IsNullOrEmpty(txtSuppliedAmount.Text))
//            {
//                txtSuppliedAmount.Text = "1";
//            }

//            // Default discount to 0% if empty
//            if (string.IsNullOrEmpty(txtDiscount.Text))
//            {
//                txtDiscount.Text = "0%";
//            }
//        }

//        private void UpdateSupplierDetails(SupplierDetailDTO supplier)
//        {
//            // Update the current supplier detail object with selected supplier info
//            _currentSupplierDetail.SupplierID = supplier.SupplierID;
//            _currentSupplierDetail.SupplierName = supplier.SupplierName;
//            _currentSupplierDetail.PhoneNumber = supplier.PhoneNumber;

//            // Update UI fields
//            txtSupplierName.Text = supplier.SupplierName;

//            // If the supplier has a product associated, update that info too
//            if (supplier.ProductID > 0)
//            {
//                var associatedProduct = _supplierDto.Products.FirstOrDefault(p => p.ProductID == supplier.ProductID);
//                if (associatedProduct != null)
//                {
//                    UpdateProductDetails(associatedProduct);

//                    // Also update supplier-specific values
//                    txtPrice.Text = supplier.Price.ToString();
//                    txtSuppliedAmount.Text = supplier.SuppliedAmount.ToString();
//                    txtDiscount.Text = (supplier.Discount * 100).ToString("F0") + "%";
//                }
//            }
//        }

//        private bool ValidateSupplierInputs()
//        {
//            return ValidateRequiredFields() && ValidateNumericInputs() && ValidateStockAvailability();
//        }

//        private bool ValidateRequiredFields()
//        {
//            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
//            {
//                ShowWarningMessage("Please enter or select a supplier name");
//                return false;
//            }

//            if (string.IsNullOrWhiteSpace(txtProductName.Text) || _currentSupplierDetail.ProductID == 0)
//            {
//                ShowWarningMessage("Please select a product");
//                return false;
//            }

//            return true;
//        }

//        private bool ValidateNumericInputs()
//        {
//            if (!int.TryParse(txtPrice.Text, out _))
//            {
//                ShowWarningMessage("Please enter a valid price (whole number)");
//                return false;
//            }

//            if (!int.TryParse(txtSuppliedAmount.Text, out _))
//            {
//                ShowWarningMessage("Please enter a valid quantity (whole number)");
//                return false;
//            }

//            // Parse discount as percentage
//            string discountText = txtDiscount.Text.TrimEnd('%');
//            if (!float.TryParse(discountText, out float discountValue) || discountValue < 0 || discountValue > 100)
//            {
//                ShowWarningMessage("Please enter a valid discount percentage (0-100)");
//                return false;
//            }

//            return true;
//        }

//        private bool ValidateStockAvailability()
//        {
//            if (!int.TryParse(txtSuppliedAmount.Text, out int suppliedAmount) || suppliedAmount <= 0)
//            {
//                ShowWarningMessage("Supplied amount must be greater than zero");
//                return false;
//            }

//            // For update mode, we need to handle stock differently
//            if (_isUpdateMode)
//            {
//                // Get the original supplied amount to calculate the net change
//                int originalAmount = _currentSupplierDetail.SuppliedAmount;
//                int netChange = suppliedAmount - originalAmount;

//                // If we're increasing the supply, check if we have enough stock
//                if (netChange > 0 && netChange > _currentSupplierDetail.CurrentStock)
//                {
//                    ShowWarningMessage($"Insufficient stock. Available: {_currentSupplierDetail.CurrentStock}, Requested increase: {netChange}");
//                    return false;
//                }
//            }
//            else
//            {
//                // For new supplies, just check if we have enough stock
//                int currentStock = int.Parse(txtCurrentStock.Text);
//                if (suppliedAmount > currentStock)
//                {
//                    ShowWarningMessage($"Insufficient stock. Available: {currentStock}, Requested: {suppliedAmount}");
//                    return false;
//                }
//            }

//            return true;
//        }

//        private void ProcessSupplierOperation()
//        {
//            try
//            {
//                // Parse input values
//                int price = int.Parse(txtPrice.Text);
//                int suppliedAmount = int.Parse(txtSuppliedAmount.Text);

//                // Parse discount percentage (removing % sign if present)
//                string discountText = txtDiscount.Text.TrimEnd('%');
//                float discountPercent = float.Parse(discountText);
//                float discountFraction = discountPercent / 100f;

//                // Update the supplier detail object
//                _currentSupplierDetail.Price = price;
//                _currentSupplierDetail.SuppliedAmount = suppliedAmount;
//                _currentSupplierDetail.Discount = discountFraction;

//                // For new suppliers, make sure we have the name
//                if (!_isUpdateMode && _currentSupplierDetail.SupplierID == 0)
//                {
//                    _currentSupplierDetail.SupplierName = txtSupplierName.Text;
//                }

//                // Perform database operation
//                bool operationResult = _isUpdateMode
//                    ? _supplierBll.Update(_currentSupplierDetail)
//                    : _supplierBll.Insert(_currentSupplierDetail);

//                HandleOperationResult(operationResult);
//            }
//            catch (Exception ex)
//            {
//                ShowErrorMessage($"Operation failed: {ex.Message}");
//            }
//        }

//        private void HandleOperationResult(bool success)
//        {
//            if (success)
//            {
//                ShowSuccessMessage(_isUpdateMode ? "Supplier updated successfully" : "Supplier added successfully");
//                RefreshData();

//                if (!_isUpdateMode)
//                {
//                    ResetFormFields();
//                }
//            }
//            else
//            {
//                ShowWarningMessage("Operation did not complete successfully");
//            }
//        }

//        private void RefreshData()
//        {
//            _supplierDto = _supplierBll.Select();
//            gridProduct.DataSource = _supplierDto.Products;
//            gridSupplier.DataSource = _supplierDto.Suppliers;
//            gridProduct.Refresh();
//            gridSupplier.Refresh();
//        }

//        #endregion

//        #region Helper Methods

//        private void ResetFormFields()
//        {
//            txtSupplierName.Clear();
//            txtProductName.Clear();
//            txtPrice.Clear();
//            txtSuppliedAmount.Clear();
//            txtDiscount.Text = "0%";
//            txtCurrentStock.Clear();
//            _currentSupplierDetail = new SupplierDetailDTO();
//        }

//        private bool IsValidGridSelection(DataGridView grid, int rowIndex)
//        {
//            return rowIndex >= 0 && rowIndex < grid.Rows.Count && grid.Rows[rowIndex].DataBoundItem != null;
//        }

//        private void ShowErrorMessage(string message)
//        {
//            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }

//        private void ShowWarningMessage(string message)
//        {
//            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        }

//        private void ShowSuccessMessage(string message)
//        {
//            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        #endregion
//    }
//}