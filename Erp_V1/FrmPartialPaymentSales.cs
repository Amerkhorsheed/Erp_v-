using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1
{
    public partial class FrmPartialPaymentSales : XtraForm
    {
        #region Fields and Properties
        private SalesBLL salesBll = new SalesBLL();
        private CustomerBLL customerBll = new CustomerBLL();
        private SalesDTO salesDto = new SalesDTO();
        private CustomerDTO customerDto = new CustomerDTO();
        private List<SalesDetailDTO> cartItems = new List<SalesDetailDTO>();
        private decimal totalAmount = 0;
        private int currentSalesId = 0;
        #endregion

        #region Constructor
        public FrmPartialPaymentSales()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void FrmPartialPaymentSales_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitializeControls();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Data Loading Methods
        private void LoadData()
        {
            salesDto = salesBll.Select();
            customerDto = customerBll.Select();

            // Setup product grid
            gridProducts.DataSource = salesDto.Products;
            ConfigureProductGrid();

            // Setup customer grid
            gridCustomers.DataSource = customerDto.Customers;
            ConfigureCustomerGrid();

            // Setup cart grid
            gridCart.DataSource = cartItems;
            ConfigureCartGrid();
        }

        private void ConfigureProductGrid()
        {
            gridProducts.Columns["ProductName"].HeaderText = "Product Name";
            gridProducts.Columns["CategoryName"].HeaderText = "Category";
            gridProducts.Columns["stockAmount"].HeaderText = "Stock";
            gridProducts.Columns["Sale_Price"].HeaderText = "Price";
            gridProducts.Columns["ProductID"].Visible = false;
            gridProducts.Columns["CategoryID"].Visible = false;
            gridProducts.Columns["price"].Visible = false;
            gridProducts.Columns["isCategoryDeleted"].Visible = false;
            gridProducts.Columns["isDeleted"].Visible = false;
            gridProducts.Columns["MinQty"].HeaderText = "Min Qty";
            gridProducts.Columns["MaxDiscount"].HeaderText = "Max Discount";
        }

        private void ConfigureCustomerGrid()
        {
            if (gridCustomers.Columns.Count > 0)
                gridCustomers.Columns[0].Visible = false;
            if (gridCustomers.Columns.Count > 1)
                gridCustomers.Columns[1].HeaderText = "Customer Name";
            if (gridCustomers.Columns.Count > 2)
                gridCustomers.Columns[2].Visible = false;
            if (gridCustomers.Columns.Count > 3)
                gridCustomers.Columns[3].HeaderText = "Phone";
        }

        private void ConfigureCartGrid()
        {
            gridCart.AutoGenerateColumns = false;
            gridCart.Columns.Clear();
            
            gridCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Product",
                Width = 150
            });
            
            gridCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SalesAmount",
                HeaderText = "Quantity",
                Width = 80
            });
            
            gridCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Unit Price",
                Width = 100
            });
            
            gridCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                Width = 100,
                ReadOnly = true
            });
            
            var removeColumn = new DataGridViewButtonColumn
            {
                Name = "Remove",
                HeaderText = "Action",
                Text = "Remove",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            gridCart.Columns.Add(removeColumn);
        }

        private void InitializeControls()
        {
            // Initialize payment options
            rbFullPayment.Checked = true;
            rbPartialPayment.Checked = false;
            rbNoPayment.Checked = false;
            
            // Initialize payment fields
            txtTotalAmount.ReadOnly = true;
            txtPaidAmount.Text = "0";
            txtRemainingAmount.ReadOnly = true;
            
            // Set date to today
            dtSalesDate.Value = DateTime.Now;
            
            UpdatePaymentControls();
        }
        #endregion

        #region Product Selection
        private void gridProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= gridProducts.Rows.Count)
                    return;

                var row = gridProducts.Rows[e.RowIndex];
                var product = row.DataBoundItem as ProductDetailDTO;
                if (product != null)
                {
                    txtSelectedProduct.Text = product.ProductName;
                    txtProductPrice.Text = product.Sale_Price.ToString("F2");
                    txtAvailableStock.Text = product.stockAmount.ToString();
                    numQuantity.Maximum = product.stockAmount;
                    numQuantity.Value = 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error selecting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Customer Selection
        private void gridCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= gridCustomers.Rows.Count)
                    return;

                var row = gridCustomers.Rows[e.RowIndex];
                var customer = row.DataBoundItem as CustomerDetailDTO;
                if (customer != null)
                {
                    txtSelectedCustomer.Text = customer.CustomerName;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error selecting customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Cart Management
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridProducts.CurrentRow?.DataBoundItem is ProductDetailDTO product)
                {
                    var quantity = (int)numQuantity.Value;
                    if (quantity <= 0)
                    {
                        XtraMessageBox.Show("Please enter a valid quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (quantity > product.stockAmount)
                    {
                        XtraMessageBox.Show("Insufficient stock available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if product already exists in cart
                    var existingItem = cartItems.FirstOrDefault(x => x.ProductID == product.ProductID);
                    if (existingItem != null)
                    {
                        existingItem.SalesAmount += quantity;
                    }
                    else
                    {
                        var cartItem = new SalesDetailDTO
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            CategoryID = product.CategoryID,
                            CategoryName = product.CategoryName,
                            SalesAmount = quantity,
                            Price = (int)product.Sale_Price,
                            StockAmount = product.stockAmount,
                            MaxDiscount = product.MaxDiscount,
                            MinQty = product.MinQty
                        };
                        cartItems.Add(cartItem);
                    }

                    RefreshCart();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error adding to cart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == gridCart.Columns["Remove"].Index && e.RowIndex >= 0)
                {
                    cartItems.RemoveAt(e.RowIndex);
                    RefreshCart();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error removing item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshCart()
        {
            gridCart.DataSource = null;
            gridCart.DataSource = cartItems;
            
            // Calculate totals for each row
            foreach (DataGridViewRow row in gridCart.Rows)
            {
                if (row.DataBoundItem is SalesDetailDTO item)
                {
                    row.Cells["Total"].Value = (item.Price * item.SalesAmount).ToString("F2");
                }
            }
            
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            totalAmount = cartItems.Sum(x => x.Price * x.SalesAmount);
            txtTotalAmount.Text = totalAmount.ToString("F2");
            UpdatePaymentControls();
        }
        #endregion

        #region Payment Controls
        private void rbFullPayment_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePaymentControls();
        }

        private void rbPartialPayment_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePaymentControls();
        }

        private void rbNoPayment_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePaymentControls();
        }

        private void UpdatePaymentControls()
        {
            if (rbFullPayment.Checked)
            {
                txtPaidAmount.Text = totalAmount.ToString("F2");
                txtPaidAmount.ReadOnly = true;
                txtRemainingAmount.Text = "0.00";
            }
            else if (rbPartialPayment.Checked)
            {
                txtPaidAmount.ReadOnly = false;
                if (string.IsNullOrEmpty(txtPaidAmount.Text) || txtPaidAmount.Text == "0")
                    txtPaidAmount.Text = "0.00";
                CalculateRemaining();
            }
            else if (rbNoPayment.Checked)
            {
                txtPaidAmount.Text = "0.00";
                txtPaidAmount.ReadOnly = true;
                txtRemainingAmount.Text = totalAmount.ToString("F2");
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (rbPartialPayment.Checked)
            {
                CalculateRemaining();
            }
        }

        private void CalculateRemaining()
        {
            if (decimal.TryParse(txtPaidAmount.Text, out decimal paidAmount))
            {
                var remaining = totalAmount - paidAmount;
                txtRemainingAmount.Text = Math.Max(0, remaining).ToString("F2");
            }
            else
            {
                txtRemainingAmount.Text = totalAmount.ToString("F2");
            }
        }
        #endregion

        #region Sales Processing
        private void btnProcessSale_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateSale())
                    return;

                ProcessSalesTransaction();
                
                XtraMessageBox.Show("Sale processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error processing sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateSale()
        {
            if (cartItems.Count == 0)
            {
                XtraMessageBox.Show("Please add items to cart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (gridCustomers.CurrentRow?.DataBoundItem == null)
            {
                XtraMessageBox.Show("Please select a customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbPartialPayment.Checked)
            {
                if (!decimal.TryParse(txtPaidAmount.Text, out decimal paidAmount) || paidAmount < 0)
                {
                    XtraMessageBox.Show("Please enter a valid paid amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (paidAmount > totalAmount)
                {
                    XtraMessageBox.Show("Paid amount cannot exceed total amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void ProcessSalesTransaction()
        {
            var customer = gridCustomers.CurrentRow.DataBoundItem as CustomerDetailDTO;
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);
            decimal remainingAmount = decimal.Parse(txtRemainingAmount.Text);

            // Generate a unique sales transaction ID
            currentSalesId = GenerateTransactionId();

            foreach (var item in cartItems)
            {
                var saleDetail = new SalesDetailDTO
                {
                    CustomerID = customer.CustomerID,
                    CustomerName = customer.CustomerName,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    CategoryID = item.CategoryID,
                    CategoryName = item.CategoryName,
                    SalesAmount = item.SalesAmount,
                    Price = item.Price,
                    SalesDate = dtSalesDate.Value,
                    MaxDiscount = item.MaxDiscount,
                    Madfou3 = (int)(paidAmount * (item.Price * item.SalesAmount) / totalAmount), // Proportional payment
                    Baky = (int)(remainingAmount * (item.Price * item.SalesAmount) / totalAmount), // Proportional remaining
                    SalesID = currentSalesId // Use the same transaction ID for all items
                };

                salesBll.Insert(saleDetail);
            }
        }

        private int GenerateTransactionId()
        {
            // Generate a unique transaction ID based on timestamp
            return (int)(DateTime.Now.Ticks % int.MaxValue);
        }
        #endregion

        #region Utility Methods
        private void ClearForm()
        {
            cartItems.Clear();
            RefreshCart();
            txtSelectedProduct.Text = "";
            txtSelectedCustomer.Text = "";
            txtProductPrice.Text = "";
            txtAvailableStock.Text = "";
            numQuantity.Value = 1;
            rbFullPayment.Checked = true;
            UpdatePaymentControls();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}