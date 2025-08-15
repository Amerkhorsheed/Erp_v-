using DevExpress.XtraCharts;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class frmSupplierPurchaseReport : DevExpress.XtraEditors.XtraForm
    {
        // Business logic for purchases.
        private readonly PurchasesBLL purchasesBLL = new PurchasesBLL();
        // Dictionary to record monthly purchase totals (key: "MMMM yyyy", value: total payment).
        private readonly Dictionary<string, decimal> monthlyPurchases = new Dictionary<string, decimal>();

        public frmSupplierPurchaseReport()
        {
            InitializeComponent();
        }

        private void frmSupplierPurchaseReport_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSupplierData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading supplier purchase report: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads the list of suppliers into the combo box.
        /// </summary>
        private void LoadSupplierData()
        {
            // Retrieve the Purchase DTO which includes Suppliers.
            var dto = purchasesBLL.Select();
            cboSupplierNames.DataSource = dto.Suppliers;
            cboSupplierNames.DisplayMember = "SupplierName";
            cboSupplierNames.ValueMember = "SupplierID";
            cboSupplierNames.SelectedIndex = -1;
        }

        /// <summary>
        /// Event handler for loading purchase data for the selected supplier.
        /// </summary>
        private void btnLoadPurchases_Click(object sender, EventArgs e)
        {
            if (cboSupplierNames.SelectedItem is SupplierDetailDTO selectedSupplier)
            {
                LoadPurchaseData(selectedSupplier.SupplierID);
            }
            else
            {
                MessageBox.Show("Please select a supplier.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Loads purchase records for a given supplier, updates the grid, total payment, and monthly chart.
        /// </summary>
        /// <param name="supplierId">The supplier's ID.</param>
        private void LoadPurchaseData(int supplierId)
        {
            try
            {
                var dto = purchasesBLL.Select();
                // Filter purchase records for the selected supplier.
                var purchaseData = dto.Purchases.Where(p => p.SupplierID == supplierId).ToList();

                // Prepare data for grid display.
                var gridData = purchaseData.Select(p => new
                {
                    p.PurchaseID,
                    p.SupplierName,
                    p.ProductName,
                    p.CategoryName,
                    PurchaseAmount = p.PurchaseAmount,
                    Price = p.Price,
                    TotalCost = p.PurchaseAmount * p.Price,
                    p.PurchaseDate
                }).ToList();

                dgvPurchaseDetails.DataSource = gridData;

                // Calculate total payment.
                decimal totalPayment = gridData.Sum(x => x.TotalCost);
                txtTotalPayment.Text = totalPayment.ToString("C");

                // Aggregate monthly totals.
                monthlyPurchases.Clear();
                foreach (var purchase in purchaseData)
                {
                    decimal cost = purchase.PurchaseAmount * purchase.Price;
                    string monthYear = purchase.PurchaseDate.ToString("MMMM yyyy");
                    if (monthlyPurchases.ContainsKey(monthYear))
                        monthlyPurchases[monthYear] += cost;
                    else
                        monthlyPurchases[monthYear] = cost;
                }
                UpdatePurchaseChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading purchase data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the chart with monthly purchase payment data.
        /// </summary>
        private void UpdatePurchaseChart()
        {
            // Retrieve or create the series.
            Series paymentSeries = chartControl1.Series.FirstOrDefault(s => s.Name == "PaymentSeries") as Series;
            if (paymentSeries == null)
            {
                paymentSeries = new Series("PaymentSeries", ViewType.Bar);
                chartControl1.Series.Add(paymentSeries);
            }
            else
            {
                paymentSeries.Points.Clear();
            }

            // Order monthly totals by date.
            var orderedData = monthlyPurchases.OrderBy(e =>
            {
                DateTime dt;
                DateTime.TryParseExact(e.Key, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                return dt;
            });

            foreach (var entry in orderedData)
            {
                paymentSeries.Points.Add(new SeriesPoint(entry.Key, entry.Value));
            }

            chartControl1.Titles.Clear();
            chartControl1.Titles.Add(new ChartTitle
            {
                Text = "Monthly Purchase Payment by Supplier",
                Font = new System.Drawing.Font("Tahoma", 14, System.Drawing.FontStyle.Bold)
            });
        }
    }
}
