using Erp_V1.DAL.DTO;
using Erp_V1.ML.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    /// <inheritdoc />
    public class ChurnPredictionService : IChurnPredictionService
    {
        private readonly SalesBLL _salesBll;
        private readonly MLContext _mlContext;
        private readonly ILogger<ChurnPredictionService> _logger;
        private readonly ChurnPredictionOptions _options;
        private ITransformer _model;
        private readonly string _modelPath;

        public ChurnPredictionService(SalesBLL salesBll, IOptions<ChurnPredictionOptions> options, ILogger<ChurnPredictionService> logger)
        {
            _salesBll = salesBll;
            _options = options.Value;
            _logger = logger;
            _mlContext = new MLContext(seed: 0);
            _modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _options.ModelFileName);
        }

        public async Task TrainAndSaveModelAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Churn model training pipeline started. Threshold: {Threshold} days.", _options.ChurnThresholdInDays);

            var featureData = await Task.Run(() => PrepareFeatures(true), cancellationToken);
            if (featureData.Count < 100)
            {
                _logger.LogWarning("Insufficient data ({RecordCount} records) to train a robust model. Aborting.", featureData.Count);
                return;
            }

            var dataView = _mlContext.Data.LoadFromEnumerable(featureData);
            cancellationToken.ThrowIfCancellationRequested();

            var trainerOptions = new LightGbmBinaryTrainer.Options
            {
                LabelColumnName = "Label",
                FeatureColumnName = "Features",
                NumberOfLeaves = 40,
                MinimumExampleCountPerLeaf = 10,
                LearningRate = 0.1,
                UnbalancedSets = true
            };

            var pipeline = DefineTrainingPipeline(trainerOptions);

            LogCrossValidationMetrics(pipeline, dataView);
            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogInformation("Training final model on the full dataset...");
            var trainedModel = pipeline.Fit(dataView);
            cancellationToken.ThrowIfCancellationRequested();

            // The ExplainModelDecisions method call has been removed for compatibility.

            _logger.LogInformation("Saving model to: {ModelPath}", _modelPath);
            _mlContext.Model.Save(trainedModel, dataView.Schema, _modelPath);

            _model = trainedModel;
            _logger.LogInformation("Training pipeline completed successfully.");
        }

        public async Task<IReadOnlyList<CustomerPredictionResult>> GetChurningCustomersAsync(CancellationToken cancellationToken = default)
        {
            LoadModelIfNeeded();

            var salesData = await Task.Run(() => _salesBll.Select(), cancellationToken);
            var asOf = DateTime.Today;

            var allBaseFeatures = salesData.Customers
                .Select(c => ComputeBaseFeatures(c, salesData.Sales, asOf, false))
                .Where(f => f != null).ToList();

            if (!allBaseFeatures.Any()) return Array.Empty<CustomerPredictionResult>();

            var boundaries = ComputeRfmBoundaries(allBaseFeatures);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<CustomerFeatureData, InternalChurnPrediction>(_model);

            var predictions = salesData.Customers
                .Select(c => CreatePrediction(c, salesData.Sales, asOf, boundaries, predictionEngine))
                .Where(p => p != null && p.PredictedChurn)
                .OrderByDescending(p => p.Probability)
                .ToList();

            _logger.LogInformation("Identified {ChurnCount} customers at risk of churn.", predictions.Count);
            return predictions;
        }

        // --- All other methods remain the same ---

        #region Private Helpers

        private IEstimator<ITransformer> DefineTrainingPipeline(LightGbmBinaryTrainer.Options options)
        {
            return _mlContext.Transforms.Concatenate(
               "Features",
               nameof(CustomerFeatureData.Frequency), nameof(CustomerFeatureData.MonetaryValue),
               nameof(CustomerFeatureData.Tenure), nameof(CustomerFeatureData.AvgTransactionValue),
               nameof(CustomerFeatureData.PurchaseFrequencyDays), nameof(CustomerFeatureData.RecencyScore),
               nameof(CustomerFeatureData.FrequencyScore), nameof(CustomerFeatureData.MonetaryScore)
           ).Append(_mlContext.BinaryClassification.Trainers.LightGbm(options));
        }

        private void LogCrossValidationMetrics(IEstimator<ITransformer> pipeline, IDataView data)
        {
            _logger.LogInformation("Starting {FoldCount}-fold cross-validation.", _options.CrossValidationFolds);
            var cvResults = _mlContext.BinaryClassification.CrossValidate(data, pipeline, _options.CrossValidationFolds, "Label");

            var metrics = cvResults.Select(r => r.Metrics).ToList();
            if (!metrics.Any()) { _logger.LogError("Cross-validation failed to produce any metrics."); return; }

            _logger.LogInformation(
                "Model Cross-Validation (Average Metrics): Accuracy={Accuracy:P2}, AUC={AUC:P2}, F1={F1:P2}, Recall={Recall:P2}, Precision={Precision:P2}",
                metrics.Average(m => m.Accuracy), metrics.Average(m => m.AreaUnderRocCurve),
                metrics.Average(m => m.F1Score), metrics.Average(m => m.PositiveRecall),
                metrics.Average(m => m.PositivePrecision));
        }

        private List<CustomerFeatureData> PrepareFeatures(bool forTraining)
        {
            var salesData = _salesBll.Select();
            var today = DateTime.Today;

            var allFeatures = salesData.Customers
                .Select(c => ComputeBaseFeatures(c, salesData.Sales, today, forTraining))
                .Where(f => f != null).ToList();

            if (!allFeatures.Any()) return allFeatures;

            var rfmBoundaries = ComputeRfmBoundaries(allFeatures);
            return allFeatures.Select(f => EnrichWithAdvancedFeatures(f, rfmBoundaries)).ToList();
        }

        private CustomerFeatureData ComputeBaseFeatures(CustomerDetailDTO customer, IReadOnlyList<SalesDetailDTO> sales, DateTime asOf, bool labelData)
        {
            var custSales = sales.Where(s => s.CustomerID == customer.ID).ToList();
            if (!custSales.Any()) return null;
            var recency = (float)(asOf - custSales.Max(s => s.SalesDate)).TotalDays;
            var features = new CustomerFeatureData
            {
                Recency = recency,
                Frequency = custSales.Count,
                MonetaryValue = custSales.Sum(s => s.Price * s.SalesAmount),
                Tenure = Math.Max(1f, (float)(asOf - custSales.Min(s => s.SalesDate)).TotalDays)
            };
            if (labelData) features.Churn = recency > _options.ChurnThresholdInDays;
            return features;
        }

        private CustomerFeatureData EnrichWithAdvancedFeatures(CustomerFeatureData baseFeats, (float[] R, float[] F, float[] M) bounds)
        {
            baseFeats.AvgTransactionValue = baseFeats.Frequency > 0 ? baseFeats.MonetaryValue / baseFeats.Frequency : 0;
            baseFeats.PurchaseFrequencyDays = baseFeats.Frequency > 0 ? baseFeats.Tenure / baseFeats.Frequency : 0;
            baseFeats.RecencyScore = ScoreValue(baseFeats.Recency, bounds.R, true);
            baseFeats.FrequencyScore = ScoreValue(baseFeats.Frequency, bounds.F, false);
            baseFeats.MonetaryScore = ScoreValue(baseFeats.MonetaryValue, bounds.M, false);
            return baseFeats;
        }

        private (float[] R, float[] F, float[] M) ComputeRfmBoundaries(IEnumerable<CustomerFeatureData> data)
        {
            float[] GetQuintiles(IReadOnlyList<float> values)
            {
                if (values.Count < 5) return Array.Empty<float>();
                return values.OrderBy(v => v).Select((v, i) => new { v, i })
                    .Where(x => x.i > 0 && x.i % (values.Count / 5) == 0).Select(x => x.v)
                    .Distinct().Take(4).ToArray();
            }
            var dataList = data.ToList();
            return (
                GetQuintiles(dataList.Select(d => d.Recency).ToList()),
                GetQuintiles(dataList.Select(d => d.Frequency).ToList()),
                GetQuintiles(dataList.Select(d => d.MonetaryValue).ToList())
            );
        }

        private float ScoreValue(float value, float[] thresholds, bool isRecency)
        {
            int score = 1 + thresholds.Count(t => value >= t);
            return isRecency ? (thresholds.Length + 2) - score : score;
        }

        private CustomerPredictionResult CreatePrediction(CustomerDetailDTO customer, IReadOnlyList<SalesDetailDTO> sales, DateTime asOf, (float[], float[], float[]) boundaries, PredictionEngine<CustomerFeatureData, InternalChurnPrediction> engine)
        {
            var baseFeatures = ComputeBaseFeatures(customer, sales, asOf, false);
            if (baseFeatures == null) return null;
            var advancedFeatures = EnrichWithAdvancedFeatures(baseFeatures, boundaries);
            var prediction = engine.Predict(advancedFeatures);
            return new CustomerPredictionResult
            {
                Customer = customer,
                PredictedChurn = prediction.PredictedChurn,
                Probability = prediction.Probability
            };
        }

        private void LoadModelIfNeeded()
        {
            if (_model != null) return;
            if (!File.Exists(_modelPath))
            {
                _logger.LogError("Model file not found at {ModelPath}. Please train the model first.", _modelPath);
                throw new FileNotFoundException("Model not found.", _modelPath);
            }
            _logger.LogDebug("Loading model from {ModelPath}...", _modelPath);
            _model = _mlContext.Model.Load(_modelPath, out _);
        }

        #endregion
    }
}