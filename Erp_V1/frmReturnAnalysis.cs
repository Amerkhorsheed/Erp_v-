// frmReturnAnalysis.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using Erp_V1.BLL;
using DTO = Erp_V1.DAL.DTO;  // Alias for DTO types
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Erp_V1
{
    public partial class frmReturnAnalysis : DevExpress.XtraEditors.XtraForm
    {
        #region Constants and Fields

        private const double SILHOUETTE_THRESHOLD = 0.5;
        private const int MIN_CLUSTER_COUNT = 2;
        private const int MAX_CLUSTERS = 10;
        private const int ML_SEED = 42;
        private const int MAX_ITERATIONS = 100;

        private readonly ReturnBLL _returnBLL = new ReturnBLL();
        private readonly ReturnAnalysisBLL _analysisBLL = new ReturnAnalysisBLL();
        private readonly MLContext _mlContext = new MLContext(ML_SEED);

        // Processed return reason data.
        private List<DTO.ReturnReasonData> _processedData;

        // Cancellation token source.
        private CancellationTokenSource _cancellationTokenSource;

        // Cluster quality metrics.
        private Dictionary<int, double> _clusterMetrics = new Dictionary<int, double>();
        private Dictionary<int, double> _clusterScores;

        // Additional custom UI controls (declared here only if not in designer)
        private DevExpress.XtraEditors.SimpleButton btnAutoDetect;
        private Label lblMetrics;
        private DevExpress.XtraCharts.ChartControl chartSilhouette;

        // Field for the current ML model.
        private ITransformer _model;

        #endregion

        #region Constructor and Initialization

        public frmReturnAnalysis()
        {
            InitializeComponent();
            InitializeCustomControls();
            InitializeUI();
        }

        /// <summary>
        /// Creates and adds custom controls not managed by the designer.
        /// </summary>
        private void InitializeCustomControls()
        {
            // Create Auto-Detect button (using DevExpress control)
            btnAutoDetect = new DevExpress.XtraEditors.SimpleButton
            {
                Text = "Auto-Detect Clusters",
                Size = new Size(120, 30),
                Location = new Point(btnAnalyze.Right + 10, btnAnalyze.Top)
            };
            btnAutoDetect.Click += btnAutoDetect_Click;
            Controls.Add(btnAutoDetect);

            // Create additional label for cluster quality.
            lblMetrics = new Label
            {
                AutoSize = true,
                Location = new Point(numClusterCount.Right + 15, numClusterCount.Top),
                Text = "Cluster quality: N/A"
            };
            Controls.Add(lblMetrics);

            // Create a chart for silhouette metrics.
            chartSilhouette = new DevExpress.XtraCharts.ChartControl
            {
                Dock = DockStyle.Bottom,
                Height = 150,
                Visible = false
            };
            chartSilhouette.Titles.Add(new ChartTitle { Text = "Cluster Quality Metrics" });
            Controls.Add(chartSilhouette);
        }

        /// <summary>
        /// Additional UI initialization if needed.
        /// </summary>
        private void InitializeUI()
        {
            MinimumSize = new Size(900, 700);
        }

        #endregion

        #region Event Handlers

        private async void frmReturnAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                await InitializeAnalysisDataAsync();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Initialization Error");
            }
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                ToggleAnalysisUI(false);
                int clusterCount = (int)numClusterCount.Value;
                await PerformClusterAnalysisAsync(clusterCount, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                UpdateStatus("Analysis canceled by user");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Analysis Error");
            }
            finally
            {
                ToggleAnalysisUI(true);
            }
        }

        private async void btnAutoDetect_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleAnalysisUI(false);
                UpdateStatus("Calculating optimal cluster count...");

                // Ensure processed data is available.
                if (_processedData == null || !_processedData.Any())
                {
                    UpdateStatus("Loading return data...");
                    var returnData =  _returnBLL.Select();
                    var returns = returnData?.Returns ?? new List<DTO.ReturnDetailDTO>();

                    if (!returns.Any())
                    {
                        MessageBox.Show("No return reason data found.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    UpdateStatus("Processing return reasons...");
                    _processedData = await _analysisBLL.ProcessReturnReasonsAsync(returns);
                }

                // Get optimal clusters with metrics.
                UpdateStatus("Analyzing cluster quality...");
                var (optimalK, metrics) = await _analysisBLL.FindOptimalClusterCountAsync(
                    _processedData,
                    MAX_CLUSTERS,
                    CancellationToken.None
                );

                _clusterScores = metrics;
                numClusterCount.Value = optimalK;
                UpdateStatus($"Optimal clusters detected: {optimalK}");
                DisplayClusterMetricsChart();
            }
            catch (OperationCanceledException)
            {
                UpdateStatus("Cluster detection canceled");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Auto-Detection Error");
            }
            finally
            {
                ToggleAnalysisUI(true);
            }
        }

        private async void numClusterCount_ValueChanged(object sender, EventArgs e)
        {
            numClusterCount.Value = Clamp((int)numClusterCount.Value, MIN_CLUSTER_COUNT, MAX_CLUSTERS);

            if (dgvAnalysis.DataSource != null)
            {
                if (_clusterMetrics.TryGetValue((int)numClusterCount.Value, out double metric))
                    UpdateMetricsLabel(metric);
                else
                    lblMetrics.Text = "Cluster quality: N/A";

                if (_processedData != null && _processedData.Any())
                {
                    try
                    {
                        ToggleAnalysisUI(false);
                        await PerformQuickReclusterAsync((int)numClusterCount.Value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Quick recluster error: {ex.Message}");
                    }
                    finally
                    {
                        ToggleAnalysisUI(true);
                    }
                }
            }
        }

        #endregion

        #region Core Analysis Logic

        /// <summary>
        /// Initializes analysis data on form load.
        /// </summary>
        private async Task InitializeAnalysisDataAsync()
        {
            UseWaitCursor = true;
            try
            {
                UpdateStatus("Loading return data...");
                await LoadReturnDataAsync();
                if (_processedData == null || !_processedData.Any())
                {
                    MessageBox.Show("No valid return data available for analysis.",
                        "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
                UpdateStatus("Ready for analysis");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Initialization Failed");
                Close();
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        /// <summary>
        /// Loads return data and processes it.
        /// </summary>
        private async Task LoadReturnDataAsync()
        {
            try
            {
                var returnData = _returnBLL.Select();
                var returns = returnData?.Returns ?? new List<DTO.ReturnDetailDTO>();
                if (returns.Any())
                    _processedData = await _analysisBLL.ProcessReturnReasonsAsync(returns);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data load error: {ex.Message}");
            }
        }

        /// <summary>
        /// Performs the cluster analysis using the selected cluster count.
        /// </summary>
        private async Task PerformClusterAnalysisAsync(int clusterCount, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            UpdateStatus($"Training model with {clusterCount} clusters...");

            var pipeline = _analysisBLL.BuildAnalysisPipeline(clusterCount);
            _model = pipeline.Fit(_mlContext.Data.LoadFromEnumerable(_processedData));

            cancellationToken.ThrowIfCancellationRequested();
            UpdateStatus("Generating predictions...");
            var predictions = _model.Transform(_mlContext.Data.LoadFromEnumerable(_processedData));
            var results = _analysisBLL.ProcessPredictions(_processedData, predictions);

            double silhouetteScore = await _analysisBLL.CalculateSilhouetteScoreAsync(results, cancellationToken);
            _clusterMetrics[clusterCount] = silhouetteScore;
            UpdateMetricsLabel(silhouetteScore);

            cancellationToken.ThrowIfCancellationRequested();
            UpdateStatus("Visualizing results...");
            DisplayResults(results);
        }

        /// <summary>
        /// Quickly re-runs clustering on UI change.
        /// </summary>
        private async Task PerformQuickReclusterAsync(int clusterCount)
        {
            var pipeline = _analysisBLL.BuildAnalysisPipeline(clusterCount);
            _model = pipeline.Fit(_mlContext.Data.LoadFromEnumerable(_processedData));
            var predictions = _model.Transform(_mlContext.Data.LoadFromEnumerable(_processedData));
            var results = _analysisBLL.ProcessPredictions(_processedData, predictions);

            double score = await _analysisBLL.CalculateSilhouetteScoreAsync(results, CancellationToken.None);
            _clusterMetrics[clusterCount] = score;

            SafeInvoke(() =>
            {
                UpdateMetricsLabel(score);
                DisplayResults(results);
            });
        }

        #endregion

        #region UI Management and Visualization

        /// <summary>
        /// Enables or disables UI controls during processing.
        /// </summary>
        private void ToggleAnalysisUI(bool isEnabled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ToggleAnalysisUI(isEnabled)));
                return;
            }
            // Use the designer-declared controls for btnAnalyze, btnExport, numClusterCount, and lblStatus.
            btnAnalyze.Text = isEnabled ? "Analyze Returns" : "Cancel Analysis";
            btnAnalyze.Enabled = true;
            btnExport.Enabled = isEnabled && dgvAnalysis.Rows.Count > 0;
            numClusterCount.Enabled = isEnabled;
            btnAutoDetect.Enabled = isEnabled;
        }

        /// <summary>
        /// Updates the status label.
        /// </summary>
        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(message)));
                return;
            }
            lblStatus.Text = message;
            Refresh();
        }

        /// <summary>
        /// Updates the cluster metrics label based on the silhouette score.
        /// </summary>
        private void UpdateMetricsLabel(double score)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateMetricsLabel(score)));
                return;
            }
            string quality = score > 0.7 ? "Excellent" :
                             score > 0.5 ? "Good" :
                             score > 0.3 ? "Fair" : "Poor";
            lblMetrics.Text = $"Cluster quality: {quality} ({score:F2})";
            lblMetrics.ForeColor = score > SILHOUETTE_THRESHOLD ? Color.DarkGreen : Color.DarkOrange;
        }

        /// <summary>
        /// Configures the main chart control.
        /// </summary>
        private void ConfigureChart()
        {
            chartControl1.Series.Clear();
            var series = new Series("Cluster Distribution", ViewType.Bar)
            {
                Label = { Visible = true, TextPattern = "{V}" }
            };
            chartControl1.Series.Add(series);

            if (chartControl1.Diagram is XYDiagram diagram)
            {
                diagram.AxisX.Label.Angle = -45;
                diagram.AxisX.Title.Text = "Clusters";
                diagram.AxisX.Title.Visibility = DefaultBoolean.True;
                diagram.AxisY.Title.Text = "Number of Returns";
                diagram.AxisY.Title.Visibility = DefaultBoolean.True;
            }
            chartControl1.PaletteName = "Office";
        }

        /// <summary>
        /// Configures the grid for data display.
        /// </summary>
        private void ConfigureGrid()
        {
            dgvAnalysis.Columns.Clear();
            dgvAnalysis.AutoGenerateColumns = false;

            // Use the custom column class to avoid naming conflicts.
            var columns = new[]
            {
                new CustomDataGridViewColumn("ReturnID", "ID", 80),
                new CustomDataGridViewColumn("OriginalReason", "Reason", 200),
                new CustomDataGridViewColumn("ClusterID", "Cluster", 60),
                new CustomDataGridViewColumn("ClusterDescription", "Keywords", 150),
                new CustomDataGridViewColumn("Confidence", "Confidence", 80, "P1"),
                new CustomDataGridViewColumn("ReturnDate", "Date", 100, "d")
            };

            foreach (var col in columns)
                dgvAnalysis.Columns.Add(col.Create());

            dgvAnalysis.DataBindingComplete += (s, e) =>
            {
                if (dgvAnalysis.Columns["ClusterID"] != null && e.ListChangedType == ListChangedType.Reset)
                    dgvAnalysis.Sort(dgvAnalysis.Columns["ClusterID"], ListSortDirection.Ascending);
            };
        }

        /// <summary>
        /// Displays the metrics chart for different cluster counts.
        /// </summary>
        private void DisplayClusterMetricsChart()
        {
            if (_clusterScores == null || _clusterScores.Count < 2)
                return;
            chartSilhouette.Series.Clear();
            var series = new Series("Cluster Quality", ViewType.Line)
            {
                Label = { Visible = true, TextPattern = "{V:F2}" }
            };
            foreach (var kv in _clusterScores.OrderBy(k => k.Key))
                series.Points.Add(new SeriesPoint(kv.Key.ToString(), kv.Value));
            chartSilhouette.Series.Add(series);
            if (series.View is LineSeriesView lineView)
            {
                lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
                lineView.LineMarkerOptions.Size = 8;
                lineView.LineStyle.Thickness = 2;
            }
            if (chartSilhouette.Diagram is XYDiagram diagram)
            {
                diagram.AxisX.Title.Text = "Number of Clusters (k)";
                diagram.AxisX.Title.Visibility = DefaultBoolean.True;
                diagram.AxisY.Title.Text = "Quality Score";
                diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                diagram.AxisY.WholeRange.MinValue = 0;
                diagram.AxisY.WholeRange.MaxValue = 1;
            }
            chartSilhouette.Visible = true;
        }

        /// <summary>
        /// Displays clustering results in the grid and visualization.
        /// </summary>
        private void DisplayResults(List<DTO.ReturnAnalysisResult> results)
        {
            var clusterKeywords = _analysisBLL.IdentifyClusterKeywords(results);
            results.ForEach(r =>
                r.ClusterDescription = clusterKeywords.TryGetValue(r.ClusterID, out var keys)
                    ? string.Join(", ", keys)
                    : "Unknown");
            dgvAnalysis.DataSource = new SortableBindingList<DTO.ReturnAnalysisResult>(results);
            CreateClusterVisualization(results, clusterKeywords);
        }

        /// <summary>
        /// Creates the cluster visualization on the chart.
        /// </summary>
        private void CreateClusterVisualization(List<DTO.ReturnAnalysisResult> results, Dictionary<uint, List<string>> keywords)
        {
            chartControl1.Series.Clear();
            var series = new Series("Cluster Distribution", ViewType.Bar)
            {
                Label = { Visible = true, TextPattern = "{V}" }
            };
            foreach (var cluster in results.GroupBy(r => r.ClusterID).OrderBy(g => g.Key))
            {
                string label = $"Cluster {cluster.Key}: {string.Join(", ", keywords[(uint)cluster.Key].Take(3))}";
                series.Points.Add(new SeriesPoint(label, cluster.Count()));
            }
            chartControl1.Series.Add(series);
            if (series.View is BarSeriesView barView)
            {
                barView.ColorEach = true;
                barView.BarWidth = 0.7;
            }
        }

        #endregion

        #region Data Processing Helpers

        /// <summary>
        /// Processes predictions by combining return data with ML predictions.
        /// </summary>
        private List<DTO.ReturnAnalysisResult> ProcessPredictions(List<DTO.ReturnReasonData> data, IDataView predictions)
        {
            var predictedResults = _mlContext.Data.CreateEnumerable<DTO.ReturnClusterPrediction>(predictions, reuseRowObject: false);
            return data.Zip(predictedResults, (d, p) => new DTO.ReturnAnalysisResult
            {
                ReturnID = d.ReturnID,
                OriginalReason = d.OriginalReason,
                ProcessedReason = d.Reason,
                ClusterID = p.PredictedClusterId,
                Confidence = p.Score?.Max() ?? 0,
                ReturnDate = d.ReturnDate,
                CustomerID = d.CustomerID,
                ProductID = d.ProductID
            }).ToList();
        }

        /// <summary>
        /// Calculates the average Euclidean distance between a given feature vector and a collection of other vectors.
        /// </summary>
        private static double CalculateAverageDistance(IReadOnlyList<VBuffer<float>> featureVectors, int index, IEnumerable<int> otherIndices)
        {
            double totalDistance = 0;
            int count = 0;
            var currentVector = featureVectors[index];
            foreach (var i in otherIndices)
            {
                if (i < 0 || i >= featureVectors.Count)
                    continue;
                totalDistance += CalculateDistance(currentVector, featureVectors[i]);
                count++;
            }
            return count > 0 ? totalDistance / count : 0;
        }

        /// <summary>
        /// Computes the Euclidean distance between two feature vectors.
        /// </summary>
        private static double CalculateDistance(VBuffer<float> a, VBuffer<float> b)
        {
            var aValues = a.GetValues().ToArray();
            var bValues = b.GetValues().ToArray();
            double sum = 0;
            for (int i = 0; i < aValues.Length; i++)
            {
                double diff = aValues[i] - bValues[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Handles errors by logging and displaying a message.
        /// </summary>
        private void HandleError(Exception ex, string context)
        {
            MessageBox.Show($"{context}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            UpdateStatus("Operation failed");
        }

        /// <summary>
        /// Exports data to a file.
        /// </summary>
        private void ExportData(string fileName, int format)
        {
            var data = dgvAnalysis.DataSource as BindingList<DTO.ReturnAnalysisResult>;
            if (data == null)
                return;
            System.IO.File.WriteAllText(fileName,
                string.Join(Environment.NewLine,
                    new[] { "ReturnID,Reason,Cluster,Keywords,Confidence,Date" }
                    .Concat(data.Select(r =>
                        $"{r.ReturnID},\"{r.OriginalReason.Replace("\"", "\"\"")}\",{r.ClusterID}," +
                        $"\"{r.ClusterDescription.Replace("\"", "\"\"")}\",{r.Confidence},{r.ReturnDate:yyyy-MM-dd}"))));
        }

        /// <summary>
        /// Safely invokes an action on the UI thread.
        /// </summary>
        private void SafeInvoke(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        /// <summary>
        /// Clamps a value between the specified minimum and maximum.
        /// </summary>
        private static int Clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }

        #endregion

        #region Export and Other UI Events

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAnalysis.DataSource == null || dgvAnalysis.Rows.Count == 0)
                {
                    MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Files|*.xlsx|CSV Files|*.csv|All Files|*.*";
                    saveDialog.Title = "Export Analysis Results";
                    saveDialog.FileName = $"ReturnAnalysis_{DateTime.Now:yyyyMMdd}";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportData(saveDialog.FileName, saveDialog.FilterIndex);
                        MessageBox.Show($"Data exported to {saveDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, "Export Error");
            }
        }

        #endregion
    }
}

// Custom classes to avoid conflicts with built-in types.
namespace Erp_V1
{
    /// <summary>
    /// A custom column definition for DataGridView to avoid conflict with System.Windows.Forms.DataGridViewColumn.
    /// </summary>
    public class CustomDataGridViewColumn
    {
        public string Name { get; }
        public string Header { get; }
        public int Width { get; }
        public string Format { get; }

        public CustomDataGridViewColumn(string name, string header, int width, string format = null)
        {
            Name = name;
            Header = header;
            Width = width;
            Format = format;
        }

        public DataGridViewTextBoxColumn Create()
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = Name,
                DataPropertyName = Name,
                HeaderText = Header,
                Width = Width
            };
            if (!string.IsNullOrEmpty(Format))
            {
                col.DefaultCellStyle = new DataGridViewCellStyle { Format = Format };
            }
            return col;
        }
    }

    /// <summary>
    /// A binding list that supports sorting.
    /// </summary>
    /// <typeparam name="T">The type of elements.</typeparam>
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool isSorted;
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;
        private bool _isNotifying = false;

        public SortableBindingList() : base() { }

        public SortableBindingList(IEnumerable<T> enumerable) : base(new List<T>(enumerable)) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSorted;
        protected override PropertyDescriptor SortPropertyCore => sortProperty;
        protected override ListSortDirection SortDirectionCore => sortDirection;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if (Items is List<T> itemsList)
            {
                var comparer = new PropertyComparer<T>(prop, direction);
                itemsList.Sort(comparer);
                isSorted = true;
                sortProperty = prop;
                sortDirection = direction;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        protected override void RemoveSortCore()
        {
            isSorted = false;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (_isNotifying)
                return;
            try
            {
                _isNotifying = true;
                base.OnListChanged(e);
            }
            finally
            {
                _isNotifying = false;
            }
        }
    }

    /// <summary>
    /// Compares two objects based on a specified property.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public class PropertyComparer<T> : IComparer<T>
    {
        private readonly PropertyDescriptor property;
        private readonly ListSortDirection direction;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            this.property = property;
            this.direction = direction;
        }

        public int Compare(T x, T y)
        {
            object valueX = property.GetValue(x);
            object valueY = property.GetValue(y);
            int result;
            if (valueX == null)
                result = (valueY == null) ? 0 : -1;
            else if (valueY == null)
                result = 1;
            else if (valueX is IComparable comparableX)
                result = comparableX.CompareTo(valueY);
            else if (valueX.Equals(valueY))
                result = 0;
            else
                result = string.Compare(valueX.ToString(), valueY.ToString(), StringComparison.Ordinal);
            return direction == ListSortDirection.Ascending ? result : -result;
        }
    }
}
