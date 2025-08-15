using DevExpress.XtraCharts;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmSupplierPurchaseSummary : Form
    {
        // Business logic layer for purchases.
        private PurchasesBLL purchasesBLL = new PurchasesBLL();

        public FrmSupplierPurchaseSummary()
        {
            InitializeComponent();
        }

        private void FrmSupplierPurchaseSummary_Load(object sender, EventArgs e)
        {
            LoadSupplierSummaryChart();
        }

        /// <summary>
        /// Loads the supplier summary chart by grouping purchase data per supplier.
        /// </summary>
        private void LoadSupplierSummaryChart()
        {
            try
            {
                // Retrieve purchase data.
                var purchaseData = purchasesBLL.Select().Purchases;

                // Group purchase data by supplier.
                var groupedData = purchaseData
                    .GroupBy(p => p.SupplierName)
                    .Select(g => new
                    {
                        SupplierName = g.Key,
                        TotalQuantity = g.Sum(p => p.PurchaseAmount),
                        TotalCost = g.Sum(p => p.PurchaseAmount * p.Price)
                    })
                    .OrderBy(x => x.SupplierName)
                    .ToList();

                // Clear any existing series.
                chartControl1.Series.Clear();

                // Create a bar series for total quantity.
                Series seriesQuantity = new Series("Total Quantity", ViewType.Bar);
                // Create a line series for total cost.
                Series seriesCost = new Series("Total Cost", ViewType.Line);

                // Populate the series with data.
                foreach (var item in groupedData)
                {
                    seriesQuantity.Points.Add(new SeriesPoint(item.SupplierName, item.TotalQuantity));
                    seriesCost.Points.Add(new SeriesPoint(item.SupplierName, item.TotalCost));
                }

                // Customize series labels (optional).
                seriesQuantity.Label.TextPattern = "{A}: {V}";
                seriesCost.Label.TextPattern = "{A}: {V:C}"; // formats as currency

                // Add the series to the chart.
                chartControl1.Series.Add(seriesQuantity);
                chartControl1.Series.Add(seriesCost);

                // Customize the chart title.
                chartControl1.Titles.Clear();
                ChartTitle title = new ChartTitle
                {
                    Text = "Supplier Purchase Summary",
                    Font = new System.Drawing.Font("Tahoma", 14, System.Drawing.FontStyle.Bold)
                };
                chartControl1.Titles.Add(title);

                // Optionally adjust the diagram axes.
                if (chartControl1.Diagram is XYDiagram diagram)
                {
                    diagram.AxisX.Title.Text = "Supplier";
                    diagram.AxisY.Title.Text = "Quantity / Total Cost";
                    diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the supplier summary chart: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
