//// Forms/FrmProductPredictor.cs
//using Erp_V1.BLL;
//using Erp_V1.DAL.DTO;
//using System;
//using System.Collections.Generic;
//using System.Windows.Forms;
//using System.Data;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace Erp_V1
//{
//    public partial class FrmProductPredictor : Form
//    {
//        private readonly ProductPredictorBLL _predictor = new ProductPredictorBLL();

//        public FrmProductPredictor()
//        {
//            InitializeComponent();
//            ConfigureGrid();
//            ConfigureChart();
//        }

//        private void btnPredict_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var parameters = new PredictionParameters
//                {
//                    ForecastHorizon = (int)numHorizon.Value,
//                    ConfidenceLevel = (int)numConfidence.Value
//                };

//                var predictions = _predictor.GenerateProductForecasts(parameters);
//                dgvPredictions.DataSource = predictions;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Prediction failed: {ex.Message}", "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void ConfigureGrid()
//        {
//            dgvPredictions.AutoGenerateColumns = false;
//            dgvPredictions.Columns.AddRange(
//                new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Product" },
//                new DataGridViewTextBoxColumn { DataPropertyName = "PredictedSales", HeaderText = "Forecast" },
//                new DataGridViewTextBoxColumn { DataPropertyName = "ConfidenceInterval", HeaderText = "Confidence" },
//                new DataGridViewTextBoxColumn { DataPropertyName = "TrendSlope", HeaderText = "Trend" },
//                new DataGridViewTextBoxColumn { DataPropertyName = "ProductHealth", HeaderText = "Health" }
//            );
//        }

//        private void ConfigureChart()
//        {
//            chartPredictions.Series["Forecast"].ChartType = SeriesChartType.SplineArea;
//            chartPredictions.ChartAreas[0].AxisX.LabelStyle.Format = "dd MMM";
//            chartPredictions.ChartAreas[0].AxisY.Title = "Units";
//        }

//        private void dgvPredictions_DataSourceChanged(object sender, EventArgs e)
//        {
//            UpdateChartData();
//        }

//        private void UpdateChartData()
//        {
//            if (dgvPredictions.DataSource is List<ProductPredictionDTO> predictions)
//            {
//                chartPredictions.Series["Forecast"].Points.DataBind(
//                    predictions,
//                    "ProductName",
//                    "PredictedSales",
//                    "Tooltip=ConfidenceInterval"
//                );
//            }
//        }

//        protected override void OnFormClosing(FormClosingEventArgs e)
//        {
//            _predictor.Dispose();
//            base.OnFormClosing(e);
//        }
//    }
//}