using Erp_V1.BLL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Erp_V1
{
    public partial class FrmProductPredictorModern : Form
    {
        private readonly IProductPredictorService _predictor;
        private List<ProductPredictionDTO> _currentPredictions;

        // Constructor that accepts a predictor service (for DI)
        public FrmProductPredictorModern(IProductPredictorService predictor = null)
        {
            InitializeComponent();
            _predictor = predictor ?? new ProductPredictorService(new SalesDAO(), new ProductDAO()); // Pass DAO instances here

            // Wire the Predict button click event
            btnPredict.Click += (s, e) => GeneratePredictions(
                numHorizon.Value,
                numConfidence.Value,
                numSeasonality.Value,
                numTrendWindow.Value
            );

            InitializeDynamicUI();
        }

        private void InitializeDynamicUI()
        {
            Text = "Product Forecast Predictor";
            Size = new Size(1200, 800);
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 7);

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };

            this.Controls.Clear();
            this.Controls.Add(mainLayout);

            FlowLayoutPanel parametersPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };

            parametersPanel.Controls.Add(numHorizon);
            parametersPanel.Controls.Add(numConfidence);
            parametersPanel.Controls.Add(numSeasonality);
            parametersPanel.Controls.Add(numTrendWindow);
            parametersPanel.Controls.Add(btnPredict);

            SplitContainer resultsContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal
            };

            DataGridView dgvPredictionsLocal = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            ConfigureDataGridView(dgvPredictionsLocal);
            resultsContainer.Panel1.Controls.Add(dgvPredictionsLocal);

            Chart chartPredictionsLocal = new Chart
            {
                Dock = DockStyle.Fill
            };
            ConfigureChart(chartPredictionsLocal);
            resultsContainer.Panel2.Controls.Add(chartPredictionsLocal);

            mainLayout.Controls.Add(parametersPanel, 0, 0);
            mainLayout.Controls.Add(resultsContainer, 0, 1);
        }

        private void ConfigureDataGridView(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductName",
                    HeaderText = "Product",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PredictedSales",
                    HeaderText = "Forecast",
                    DefaultCellStyle = { Format = "N2" }
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ConfidenceIntervalLower",
                    HeaderText = "Conf. Lower",
                    DefaultCellStyle = { Format = "N2" }
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ConfidenceIntervalUpper",
                    HeaderText = "Conf. Upper",
                    DefaultCellStyle = { Format = "N2" }
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductHealth",
                    HeaderText = "Health Status"
                }
            });
        }

        private void ConfigureChart(Chart chart)
        {
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(new ChartArea("PredictionArea"));
            chart.Series.Clear();
            Series forecastSeries = new Series("Forecast")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.DodgerBlue
            };
            chart.Series.Add(forecastSeries);
        }

        private void GeneratePredictions(decimal horizon, decimal confidence, decimal seasonality, decimal trendWindow)
        {
            try
            {
                var parameters = new PredictionParameters
                {
                    ForecastHorizon = (int)horizon,
                    ConfidenceLevel = (int)confidence,
                    SeasonalityPeriod = (int)seasonality,
                    TrendWindow = (int)trendWindow,
                    MinimumDataPoints = 10
                };

                _currentPredictions = _predictor.GenerateProductForecasts(parameters);

                if (_currentPredictions == null || _currentPredictions.Count == 0)
                {
                    MessageBox.Show(
                        "No predictions could be generated. Please check your data sources.",
                        "No Results",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                var dgvPredictionsLocal = this.Controls.OfType<TableLayoutPanel>()
                    .First().Controls.OfType<SplitContainer>().First().Panel1.Controls.OfType<DataGridView>().First();
                dgvPredictionsLocal.DataSource = _currentPredictions;

                var chartPredictionsLocal = this.Controls.OfType<TableLayoutPanel>()
                    .First().Controls.OfType<SplitContainer>().First().Panel2.Controls.OfType<Chart>().First();
                UpdateChartData(chartPredictionsLocal, _currentPredictions);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error generating predictions:\n{ex.Message}",
                    "Prediction Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void UpdateChartData(Chart chart, List<ProductPredictionDTO> predictions)
        {
            var forecastSeries = chart.Series["Forecast"];
            forecastSeries.Points.Clear();

            var topProducts = predictions.OrderByDescending(p => p.PredictionScore)
                                            .Take(10)
                                            .ToList();

            foreach (var product in topProducts)
            {
                DataPoint dp = forecastSeries.Points.Add(product.PredictedSales);
                dp.AxisLabel = product.ProductName;
                dp.Color = product.ProductHealth == "Healthy" ? Color.Green : Color.Orange;
            }

            chart.ChartAreas[0].RecalculateAxesScale();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_predictor is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void FrmProductPredictorModern_Load(object sender, EventArgs e)
        {

        }
    }
}