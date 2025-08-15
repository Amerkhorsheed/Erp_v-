//using Erp_V1.BLL;
//using Erp_V1.DAL;
//using Erp_V1.DAL.DTO;
//using Erp_V1.ML;
//using Erp_V1.ML.DTO;
//using Microsoft.Extensions.Logging.Abstractions;
//using Microsoft.Extensions.Options;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;
//using Xunit.Abstractions;

//namespace Erp_V1.Tests
//{
//    /// <summary>
//    /// Provides a production-grade stress and quality assurance test suite for the ChurnPredictionService.
//    /// This test is designed to be comprehensive, robust, and maintainable.
//    /// </summary>
//    public sealed class ChurnPredictionServiceTests : IDisposable
//    {
//        #region Test Configuration
//        private const int CUSTOMER_COUNT = 100_000;
//        private const double REQUIRED_RECALL = 0.90;
//        private const double REQUIRED_PRECISION = 0.90;
//        private readonly TimeSpan TEST_TIMEOUT = TimeSpan.FromMinutes(120); // 2-hour timeout for this heavy test
//        #endregion

//        #region Dependencies & System Under Test (SUT)
//        private readonly ITestOutputHelper _output;
//        private readonly Mock<SalesBLL> _mockSalesBll;
//        private readonly IChurnPredictionService _sut;
//        private readonly ChurnPredictionOptions _options;
//        private readonly Stopwatch _stopwatch = new Stopwatch();
//        #endregion

//        public ChurnPredictionServiceTests(ITestOutputHelper output)
//        {
//            _output = output;
//            _mockSalesBll = new Mock<SalesBLL>(MockBehavior.Strict);

//            _options = new ChurnPredictionOptions
//            {
//                ChurnThresholdInDays = 90,
//                ModelFileName = $"test_model_{Guid.NewGuid()}.zip" // Ensures test isolation
//            };

//            var mockOptions = Options.Create(_options);
//            var nullLogger = NullLogger<ChurnPredictionService>.Instance;

//            _sut = new ChurnPredictionService(_mockSalesBll.Object, mockOptions, nullLogger);

//            Cleanup();
//        }

//        [Fact]
//        [Trait("Category", "Performance")]
//        public async Task ChurnPrediction_WithLargeRealisticDataset_ShouldMeetPerformanceAndQualityThresholds()
//        {
//            // --- ARRANGE ---
//            _output.WriteLine($"--- Starting Test: Churn Prediction Performance & Quality ---");
//            var dataGen = new TestDataGenerator(_output, CUSTOMER_COUNT, _options.ChurnThresholdInDays);
//            var (salesDto, trueChurnerIds) = dataGen.Generate();
//            _mockSalesBll.Setup(b => b.Select()).Returns(salesDto);

//            IReadOnlyList<CustomerPredictionResult> atRiskResults;
//            TimeSpan trainingTime, predictionTime;

//            // --- ACT ---
//            _stopwatch.Start();
//            using (var cts = new CancellationTokenSource(TEST_TIMEOUT))
//            {
//                await _sut.TrainAndSaveModelAsync(cts.Token);
//                trainingTime = _stopwatch.Elapsed;

//                atRiskResults = await _sut.GetChurningCustomersAsync(cts.Token);
//                predictionTime = _stopwatch.Elapsed - trainingTime;
//            }
//            _stopwatch.Stop();
//            LogPerformanceSummary(trainingTime, predictionTime);

//            // --- ASSERT ---
//            // 1. Basic Assertions
//            Assert.True(_stopwatch.Elapsed < TEST_TIMEOUT, $"Total execution time exceeded timeout of {TEST_TIMEOUT.TotalMinutes} minutes.");
//            Assert.NotNull(atRiskResults);

//            // 2. Quality Metrics Calculation
//            var predictedChurnerIds = atRiskResults.Select(r => r.Customer.ID).ToHashSet();
//            var (tp, fp, fn) = CalculateConfusionMatrix(predictedChurnerIds, trueChurnerIds);

//            double recall = trueChurnerIds.Any() ? (double)tp / trueChurnerIds.Count : 1.0;
//            double precision = predictedChurnerIds.Any() ? (double)tp / predictedChurnerIds.Count : 1.0;

//            LogQualityReport(trueChurnerIds.Count, predictedChurnerIds.Count, tp, fp, fn, recall, precision);

//            // 3. Final Quality Assertions
//            Assert.True(recall >= REQUIRED_RECALL, $"Recall [{recall:P2}] failed to meet required threshold [{REQUIRED_RECALL:P2}]. Model missed {fn:N0} churners.");
//            Assert.True(precision >= REQUIRED_PRECISION, $"Precision [{precision:P2}] failed to meet required threshold [{REQUIRED_PRECISION:P2}]. Model incorrectly flagged {fp:N0} customers.");
//        }

//        #region Helpers & Cleanup

//        public void Dispose()
//        {
//            Cleanup();
//        }

//        private void Cleanup()
//        {
//            var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _options.ModelFileName);
//            if (!File.Exists(modelPath)) return;
//            try
//            {
//                File.Delete(modelPath);
//            }
//            catch (IOException ex)
//            {
//                _output.WriteLine($"[WARNING] Could not delete test model file: {ex.Message}");
//            }
//        }

//        private (int tp, int fp, int fn) CalculateConfusionMatrix(HashSet<int> predicted, HashSet<int> actual)
//        {
//            int truePositives = predicted.Intersect(actual).Count();
//            int falsePositives = predicted.Except(actual).Count();
//            int falseNegatives = actual.Except(predicted).Count();
//            return (truePositives, falsePositives, falseNegatives);
//        }

//        private void LogPerformanceSummary(TimeSpan training, TimeSpan prediction)
//        {
//            _output.WriteLine($"\n--- Performance Summary ---");
//            _output.WriteLine($"Model Training Time   : {training.TotalMinutes:F1}m ({training.TotalSeconds:F2}s)");
//            _output.WriteLine($"Prediction Time       : {prediction.TotalSeconds:F2}s");
//            _output.WriteLine($"Total Execution Time  : {_stopwatch.Elapsed.TotalMinutes:F1}m");
//        }

//        private void LogQualityReport(int actual, int predicted, int tp, int fp, int fn, double recall, double precision)
//        {
//            _output.WriteLine($"\n--- Prediction Quality Report ---");
//            _output.WriteLine($"Actual Churners       : {actual:N0}");
//            _output.WriteLine($"Predicted Churners    : {predicted:N0}");
//            _output.WriteLine($"  - True Positives    : {tp:N0}");
//            _output.WriteLine($"  - False Positives   : {fp:N0}");
//            _output.WriteLine($"  - False Negatives   : {fn:N0}");
//            _output.WriteLine("-----------------------------------");
//            _output.WriteLine($"Recall                : {recall:P2} (Required: >= {REQUIRED_RECALL:P2})");
//            _output.WriteLine($"Precision             : {precision:P2} (Required: >= {REQUIRED_PRECISION:P2})");
//        }
//        #endregion
//    }

//    /// <summary>
//    /// Encapsulates complex, realistic data generation logic to keep the main test class clean.
//    /// </summary>
//    internal class TestDataGenerator
//    {
//        private readonly ITestOutputHelper _output;
//        private readonly int _customerCount;
//        private readonly int _churnThreshold;

//        private class CustomerProfile { public readonly int MinR, MaxR, MinF, MaxF; public CustomerProfile(int r1, int r2, int f1, int f2) { MinR = r1; MaxR = r2; MinF = f1; MaxF = f2; } }

//        internal TestDataGenerator(ITestOutputHelper output, int custCount, int churnThresh) { _output = output; _customerCount = custCount; _churnThreshold = churnThresh; }

//        internal (SalesDTO, HashSet<int>) Generate()
//        {
//            _output.WriteLine("\n--- Generating Test Data ---");
//            var customers = new List<CustomerDetailDTO>(); var sales = new List<SalesDetailDTO>();
//            var trueChurners = new HashSet<int>(); var random = new Random(); var today = DateTime.Today;

//            var profiles = new Dictionary<string, CustomerProfile>
//            {
//                ["Loyal"] = new CustomerProfile(1, 60, 15, 50),
//                ["Active"] = new CustomerProfile(5, 80, 5, 25),
//                ["AtRisk"] = new CustomerProfile(81, 100, 2, 8),
//                ["Churner"] = new CustomerProfile(_churnThreshold + 1, 365, 1, 5),
//                ["Wildcard"] = new CustomerProfile(0, 0, 0, 0)
//            };

//            for (int i = 1; i <= _customerCount; i++)
//            {
//                var c = new CustomerDetailDTO { ID = i, CustomerName = $"Customer {i}" };
//                customers.Add(c);
//                var pKey = GetRandomProfileKey(random);
//                if (pKey == "Wildcard") GenerateAnomaly(c.ID, random, today, sales);
//                else GenerateStandardProfile(c.ID, profiles[pKey], random, today, sales);
//                if (sales.Any(s => s.CustomerID == c.ID))
//                {
//                    if ((today - sales.Where(s => s.CustomerID == c.ID).Max(s => s.SalesDate)).TotalDays > _churnThreshold)
//                        trueChurners.Add(c.ID);
//                }
//            }
//            for (int i = 0; i < 5; i++) customers.Add(new CustomerDetailDTO { ID = _customerCount + i + 1, CustomerName = $"Ghost {i}" });
//            _output.WriteLine($"Data generation complete. Created {sales.Count:N0} sales records for {customers.Count:N0} customers.");
//            return (new SalesDTO { Customers = customers, Sales = sales }, trueChurners);
//        }

//        private string GetRandomProfileKey(Random r) { double d = r.NextDouble(); if (d < 0.2) return "Churner"; if (d < 0.4) return "AtRisk"; if (d < 0.55) return "Loyal"; if (d < 0.6) return "Wildcard"; return "Active"; }
//        private void GenerateStandardProfile(int id, CustomerProfile p, Random r, DateTime t, List<SalesDetailDTO> s) { int f = r.Next(p.MinF, p.MaxF + 1); var d = t.AddDays(-r.Next(p.MinR, p.MaxR + 1)); for (int j = 0; j < f; j++) { s.Add(new SalesDetailDTO { CustomerID = id, SalesDate = d.AddDays(-r.Next(j * 5, (j + 1) * 40)), Price = r.NextDouble() < 0.8 ? r.Next(5, 100) : r.Next(100, 1200), SalesAmount = r.Next(1, 4) }); } }
//        private void GenerateAnomaly(int id, Random r, DateTime t, List<SalesDetailDTO> s) { int type = r.Next(1, 4); if (type == 1) { s.Add(new SalesDetailDTO { CustomerID = id, SalesDate = t.AddDays(-r.Next(1, 15)), Price = r.Next(50, 200), SalesAmount = 1 }); s.Add(new SalesDetailDTO { CustomerID = id, SalesDate = t.AddDays(-r.Next(200, 300)), Price = r.Next(10, 50), SalesAmount = 1 }); } else if (type == 2) { s.Add(new SalesDetailDTO { CustomerID = id, SalesDate = t.AddDays(-r.Next(5, 45)), Price = r.Next(1500, 5000), SalesAmount = r.Next(1, 3) }); } else { for (int j = 0; j < r.Next(40, 100); j++) s.Add(new SalesDetailDTO { CustomerID = id, SalesDate = t.AddDays(-r.Next(10, 30) - j * 2), Price = r.Next(1, 10), SalesAmount = 1 }); } }
//    }
//}