//using Erp_V1.BLL;
//using Erp_V1.DAL;
//using Erp_V1.ML;
//using Erp_V1.ML.DTO; // Required for CustomerPredictionResult
//using MaterialSkin;
//using MaterialSkin.Controls;
//using Microsoft.Extensions.Logging.Abstractions;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic; // **FIX 1: ADDED THIS REQUIRED USING DIRECTIVE**
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Erp_V1.DAL.DAL;
//using Erp_V1.BL
//namespace Erp_V1
//{
//    public partial class ChurnPredictionForm : MaterialForm
//    {
//        private readonly IChurnPredictionService _churnPredictionService;
//        private CancellationTokenSource _cancellationTokenSource;

//        public ChurnPredictionForm()
//        {
//            InitializeComponent();
//            InitializeMaterialSkin();

//            var options = Options.Create(new ChurnPredictionOptions());
//            var logger = NullLogger<ChurnPredictionService>.Instance;

//            _churnPredictionService = new ChurnPredictionService(new SalesBLL(), options, logger);
//        }

//        #region Event Handlers

//        private async void btnTrainModel_Click(object sender, EventArgs e)
//        {
//            _cancellationTokenSource = new CancellationTokenSource();
//            SetUiState(running: true, "Training model. This may take several minutes...");

//            try
//            {
//                await _churnPredictionService.TrainAndSaveModelAsync(_cancellationTokenSource.Token)
//                                             .ConfigureAwait(true);

//                MessageBox.Show(this, "Model training completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                SetUiState(running: false, "Ready. You can now get predictions.");
//            }
//            catch (OperationCanceledException)
//            {
//                SetUiState(running: false, "Operation was cancelled by the user.");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(this, $"An unexpected error occurred during training: {ex.Message}", "Training Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                SetUiState(running: false, "An error occurred. Please try again.");
//            }
//        }

//        private async void btnGetChurningCustomers_Click(object sender, EventArgs e)
//        {
//            _cancellationTokenSource = new CancellationTokenSource();
//            SetUiState(running: true, "Getting predictions from the trained model...");

//            try
//            {
//                var churningResults = await _churnPredictionService.GetChurningCustomersAsync(_cancellationTokenSource.Token)
//                                                                     .ConfigureAwait(true);

//                // **FIX 2: Call PopulateListView directly with the IReadOnlyList<> result.**
//                PopulateListView(churningResults);

//                SetUiState(running: false, $"Found {churningResults.Count} at-risk customers.");
//            }
//            catch (OperationCanceledException)
//            {
//                SetUiState(running: false, "Operation was cancelled by the user.");
//            }
//            catch (FileNotFoundException fnfEx)
//            {
//                MessageBox.Show(this, $"Model file not found. Please train the model first.\n\nDetails: {fnfEx.Message}", "Model Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                SetUiState(running: false, "Model not trained. Please train the model first.");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(this, $"An unexpected error occurred while predicting: {ex.Message}", "Prediction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                SetUiState(running: false, "An error occurred. Please try again.");
//            }
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
//            {
//                SetUiState(running: false, "Cancelling, please wait...");
//                _cancellationTokenSource.Cancel();
//                btnCancel.Enabled = false;
//            }
//        }

//        private void ChurnPredictionForm_Load(object sender, EventArgs e) => SetUiState(running: false, "Ready.");

//        private void ChurnPredictionForm_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            // Ensure any running task is safely cancelled when the form is closed.
//            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
//            {
//                _cancellationTokenSource.Cancel();
//            }
//        }

//        #endregion

//        #region UI Helpers

//        private void InitializeMaterialSkin()
//        {
//            var manager = MaterialSkinManager.Instance;
//            manager.AddFormToManage(this);
//            manager.Theme = MaterialSkinManager.Themes.LIGHT;
//            manager.ColorScheme = new ColorScheme(
//                Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500,
//                Accent.LightBlue200, TextShade.WHITE);
//        }

//        private void SetUiState(bool running, string status = null)
//        {
//            btnTrainModel.Enabled = !running;
//            btnGetChurningCustomers.Enabled = !running;
//            btnCancel.Enabled = running;
//            progressBar.Visible = running;

//            if (status != null)
//                lblStatus.Text = status;
//        }

//        private void PopulateListView(IReadOnlyList<CustomerPredictionResult> results)
//        {
//            lstChurningCustomers.Items.Clear();
//            if (results == null || !results.Any()) return;

//            lstChurningCustomers.BeginUpdate();
//            try
//            {
//                var listItems = results.Select(res => new ListViewItem(new[]
//                {
//                    res.Customer.ID.ToString(),
//                    res.Customer.CustomerName,
//                    res.Probability.ToString("P1") // "95.5%"
//                }));

//                lstChurningCustomers.Items.AddRange(listItems.ToArray());
//            }
//            finally
//            {
//                lstChurningCustomers.EndUpdate();
//            }
//        }

//        #endregion
//    }
//}