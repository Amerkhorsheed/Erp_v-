using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmProfitReport : DevExpress.XtraEditors.XtraForm
    {
        private readonly ProductBLL productBLL = new ProductBLL();
        private readonly SalesBLL salesBLL = new SalesBLL();
        // Dictionary to record monthly profits (key: "MMMM yyyy", value: total profit)
        private readonly Dictionary<string, decimal> monthlyProfits = new Dictionary<string, decimal>();

        public frmProfitReport()
        {
            InitializeComponent();
        }

        private void frmProfitReport_Load(object sender, EventArgs e)
        {
            try
            {
                LoadProductData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profit report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductData()
        {
            var products = productBLL.Select().Products;
            cboProductNames.DataSource = products;
            cboProductNames.DisplayMember = "ProductName";
            cboProductNames.ValueMember = "ProductID";
            cboProductNames.SelectedIndex = -1;
        }

        private void btnLoadSales_Click(object sender, EventArgs e)
        {
            if (cboProductNames.SelectedItem is ProductDetailDTO selectedProduct)
            {
                LoadSalesData(selectedProduct.ProductID);
            }
            else
            {
                MessageBox.Show("Please select a product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSalesData(int productId)
        {
            try
            {
                // Retrieve all sales data and filter by the selected product.
                var allSales = salesBLL.Select().Sales;
                var salesData = allSales.Where(s => s.ProductID == productId).ToList();

                // Retrieve the product details.
                var product = productBLL.Select().Products.FirstOrDefault(p => p.ProductID == productId);
                if (product == null)
                {
                    MessageBox.Show("Selected product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Clear previous monthly profits.
                monthlyProfits.Clear();

                // Prepare data for the grid.
                // CostPrice comes from product.price, SalePrice from s.ProductSalesPrice (mapped as s.Price),
                // Discount from s.MaxDiscount (using ?? 0 is handled in the DAO), and Quantity from s.ProductSalesAmout (mapped as s.SalesAmount).
                var salesDisplay = salesData.Select(s => new
                {
                    s.SalesID,
                    s.CustomerName,
                    s.ProductName,
                    s.CategoryName,
                    Quantity = s.SalesAmount,
                    CostPrice = product.price,
                    SalePrice = s.Price,
                    Discount = s.MaxDiscount,
                    // Profit = (SalePrice - CostPrice - Discount) * Quantity.
                    Profit = (((decimal)s.Price) - ((decimal)product.price) - ((decimal)s.MaxDiscount)) * s.SalesAmount,
                    s.SalesDate
                }).ToList();

                dgvSalesDetails.DataSource = salesDisplay;

                // Calculate total profit using the original cost price.
                CalculateTotalProfit(salesData, (decimal)product.price);
                UpdateProfitChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading sales data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotalProfit(List<SalesDetailDTO> salesData, decimal costPrice)
        {
            decimal totalProfit = 0;
            foreach (var sale in salesData)
            {
                // Profit per sale record = (SalePrice - CostPrice - Discount) * Quantity.
                decimal profitPerSale = (((decimal)sale.Price) - costPrice - ((decimal)sale.MaxDiscount)) * sale.SalesAmount;
                totalProfit += profitPerSale;

                // Record monthly profit using "MMMM yyyy" format.
                string monthYear = sale.SalesDate.ToString("MMMM yyyy");
                if (monthlyProfits.ContainsKey(monthYear))
                {
                    monthlyProfits[monthYear] += profitPerSale;
                }
                else
                {
                    monthlyProfits[monthYear] = profitPerSale;
                }
            }

            txtTotalProfit.Text = totalProfit.ToString("C");
        }

        private void UpdateProfitChart()
        {
            // Retrieve the series by name using LINQ and explicitly cast to Series.
            Series profitSeries = chartControl1.Series.FirstOrDefault(s => s.Name == "ProfitSeries") as Series;
            if (profitSeries == null)
            {
                profitSeries = new Series("ProfitSeries", ViewType.Bar);
                chartControl1.Series.Add(profitSeries);
            }
            else
            {
                profitSeries.Points.Clear();
            }

            // Order monthly profits by date ascending.
            var orderedProfits = monthlyProfits.OrderBy(e =>
            {
                DateTime dt;
                DateTime.TryParseExact(e.Key, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                return dt;
            });

            foreach (var entry in orderedProfits)
            {
                profitSeries.Points.Add(new SeriesPoint(entry.Key, entry.Value));
            }
        }
    }
}
