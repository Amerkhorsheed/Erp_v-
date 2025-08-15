using DevExpress.XtraCharts;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmPurchaseChart : Form
    {
        // Business logic layer for purchases.
        private PurchasesBLL purchasesBLL = new PurchasesBLL();

        public FrmPurchaseChart()
        {
            InitializeComponent();
        }

        private void FrmPurchaseChart_Load(object sender, EventArgs e)
        {
            LoadPurchaseChart();
        }

        /// <summary>
        /// Loads and displays the purchase chart by grouping purchases by product and week.
        /// </summary>
        private void LoadPurchaseChart()
        {
            try
            {
                // Retrieve purchase data from the business layer.
                var purchaseData = purchasesBLL.Select().Purchases;

                // Group the purchase data by product name and week number (based on PurchaseDate).
                var groupedData = purchaseData
                    .GroupBy(p => new
                    {
                        p.ProductName,
                        Week = GetWeekNumber(p.PurchaseDate)
                    })
                    .Select(g => new
                    {
                        g.Key.ProductName,
                        g.Key.Week,
                        PurchaseAmount = g.Sum(p => p.PurchaseAmount)
                    })
                    .OrderBy(x => x.Week)
                    .ToList();

                // Clear any existing series on the chart.
                chartControl1.Series.Clear();

                // Create a bar series for each product.
                foreach (var productGroup in groupedData.GroupBy(x => x.ProductName))
                {
                    Series series = new Series(productGroup.Key, ViewType.Bar);
                    foreach (var weekData in productGroup)
                    {
                        // Create a series point where the argument is the week and value is the total purchase amount.
                        series.Points.Add(new SeriesPoint($"Week {weekData.Week}", weekData.PurchaseAmount));
                    }
                    chartControl1.Series.Add(series);
                }

                // Set chart title.
                chartControl1.Titles.Clear();
                chartControl1.Titles.Add(new ChartTitle { Text = "Weekly Purchases by Product" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the purchase chart: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Returns the week number for a given date based on the current culture.
        /// </summary>
        private int GetWeekNumber(DateTime date)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
