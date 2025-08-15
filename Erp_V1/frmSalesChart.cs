using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmSalesChart : DevExpress.XtraEditors.XtraForm
    {
        private SalesBLL salesBLL = new SalesBLL();

        public frmSalesChart()
        {
            InitializeComponent();
        }

        private void frmSalesChart_Load(object sender, EventArgs e)
        {
            LoadSalesChart();
        }

        private void LoadSalesChart()
        {
            try
            {
                // Fetch sales data
                var salesData = salesBLL.Select().Sales;

                // Group data by product and week
                var groupedSalesData = salesData
                    .GroupBy(s => new
                    {
                        s.ProductName,
                        Week = GetWeekNumber(s.SalesDate)
                    })
                    .Select(g => new
                    {
                        g.Key.ProductName,
                        g.Key.Week,
                        SalesAmount = g.Sum(s => s.SalesAmount)
                    })
                    .OrderBy(g => g.Week)
                    .ToList();

                // Create a series for each product
                foreach (var productGroup in groupedSalesData.GroupBy(g => g.ProductName))
                {
                    Series series = new Series(productGroup.Key, ViewType.Bar);

                    foreach (var weekGroup in productGroup)
                    {
                        series.Points.Add(new SeriesPoint($"Week {weekGroup.Week}", weekGroup.SalesAmount));
                    }

                    chartControl1.Series.Add(series);
                }

                chartControl1.Titles.Add(new ChartTitle { Text = "Weekly Sales by Product" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the sales chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetWeekNumber(DateTime date)
        {
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;
            int weekNum = cul.Calendar.GetWeekOfYear(
                date,
                System.Globalization.CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
            return weekNum;
        }
    }
}
