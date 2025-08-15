using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Erp_V1.BLL;

namespace Erp_V1
{
    public partial class ProfitDashboard : Form
    {
        private readonly ProfitBLL _profitBll = new ProfitBLL();
        private readonly SalesBLL _salesBll = new SalesBLL();
        private readonly ExpensesBLL _expensesBll = new ExpensesBLL();

        public ProfitDashboard()
        {
            InitializeComponent();
            ConfigureChart();
            SetModernStyling();
        }

        private void ConfigureChart()
        {
            profitChart.Series.Clear();
            var series = new Series("Monthly Profit")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.FromArgb(0, 150, 136),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8,
                MarkerColor = Color.White
            };
            profitChart.Series.Add(series);

            profitChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            profitChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            profitChart.ChartAreas[0].BackColor = Color.Transparent;
        }

        private void SetModernStyling()
        {
            BackColor = Color.FromArgb(37, 42, 64);
            ForeColor = Color.White;

            foreach (Control control in Controls)
            {
                if (control is Panel panel)
                {
                    panel.BackColor = Color.FromArgb(46, 51, 73);
                    panel.ForeColor = Color.White;
                    panel.BorderStyle = BorderStyle.None;
                }
            }

            netProfitLabel.Font = new Font("Segoe UI", 12 , FontStyle.Bold);
            netProfitLabel.ForeColor = Color.FromArgb(0, 150, 136);
        }

        private void ProfitDashboard_Load(object sender, EventArgs e)
        {
            LoadProfitData();
            LoadHistoricalData();
        }

        private void LoadProfitData()
        {
            decimal finalProfit = _profitBll.GetFinalProfit();
            decimal totalSales = (decimal)_salesBll.Select().Sales.Sum(s => (s.Price * s.SalesAmount)-s.MaxDiscount);
            decimal totalExpenses = _expensesBll.Select().Expenses.Sum(e => e.Amount);

            netProfitLabel.Text = finalProfit.ToString("C2");
            salesAmountLabel.Text = totalSales.ToString("C2");
            expensesAmountLabel.Text = totalExpenses.ToString("C2");
        }

        private void LoadHistoricalData()
        {
            // Sample data - replace with actual historical data from your DAO
            var monthlyProfits = new[] { 15000m, 18000m, 22000m, 21000m, 25000m, 28000m };

            profitChart.Series["Monthly Profit"].Points.Clear();
            foreach (decimal profit in monthlyProfits)
            {
                profitChart.Series["Monthly Profit"].Points.AddY(profit);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProfitData();
            LoadHistoricalData();
        }
    }
}