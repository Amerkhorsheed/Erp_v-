//using DevExpress.Utils.Extensions;
//using Erp_V1.BLL;
//using Erp_V1.DAL.DAO;
//using Erp_V1.DAL.DTO;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace Erp_V1.Tests
//{
//    public class ReturnAnalysisBLLTests
//    {
//        private List<ReturnDetailDTO> GetFakeReturnDetails()
//        {
//            return new List<ReturnDetailDTO>
//            {
//                new ReturnDetailDTO { ReturnID = 1, ReturnReason = "Packaging damaged on arrival", ReturnDate = DateTime.UtcNow.AddDays(-10), CustomerID = 101, ProductID = 5001 },
//                new ReturnDetailDTO { ReturnID = 2, ReturnReason = "Product was physically damaged", ReturnDate = DateTime.UtcNow.AddDays(-9), CustomerID = 102, ProductID = 5002 },
//                new ReturnDetailDTO { ReturnID = 3, ReturnReason = "Screen damaged upon unboxing", ReturnDate = DateTime.UtcNow.AddDays(-8), CustomerID = 103, ProductID = 5003 },
//                new ReturnDetailDTO { ReturnID = 4, ReturnReason = "The size listed was too small", ReturnDate = DateTime.UtcNow.AddDays(-7), CustomerID = 104, ProductID = 5004 },
//                new ReturnDetailDTO { ReturnID = 5, ReturnReason = "Did not fit my expectations; size was wrong", ReturnDate = DateTime.UtcNow.AddDays(-6), CustomerID = 105, ProductID = 5005 },
//                new ReturnDetailDTO { ReturnID = 6, ReturnReason = "Sizing chart inaccurate, too large for me", ReturnDate = DateTime.UtcNow.AddDays(-5), CustomerID = 106, ProductID = 5006 },
//                new ReturnDetailDTO { ReturnID = 7, ReturnReason = "Color not as pictured on the website", ReturnDate = DateTime.UtcNow.AddDays(-4), CustomerID = 107, ProductID = 5007 },
//                new ReturnDetailDTO { ReturnID = 8, ReturnReason = "Wrong color shade received", ReturnDate = DateTime.UtcNow.AddDays(-3), CustomerID = 108, ProductID = 5008 },
//                new ReturnDetailDTO { ReturnID = 9, ReturnReason = "The color faded quickly compared to description", ReturnDate = DateTime.UtcNow.AddDays(-2), CustomerID = 109, ProductID = 5009 }
//            };
//        }

//        private ReturnAnalysisConfigDTO CreateTestConfig(int clusterCount = 3, HashSet<string> stopWords = null)
//        {
//            return new ReturnAnalysisConfigDTO
//            {
//                ClusterCount = clusterCount,
//                MinWordLength = 3,
//                StopWords = stopWords ?? new HashSet<string> { "on", "the", "for", "and", "was", "not", "too", "as", "compared", "my", "me", "to" },
//                MaxFeatures = 1000,
//                MaxIterations = 100,
//                SilhouetteThreshold = 0.5
//            };
//        }

//        [Fact]
//        public async Task AnalyzeReturnReasonsAsync_WithValidData_ReturnsExpectedClusterCountAndMetrics()
//        {
//            var mockDao = new Mock<ReturnDAO>();
//            mockDao.Setup(d => d.Select()).Returns(GetFakeReturnDetails());

//            var config = CreateTestConfig(clusterCount: 3);
//            var bll = new ReturnAnalysisBLL(mockDao.Object);

//            var result = await bll.AnalyzeReturnReasonsAsync(config, CancellationToken.None);

//            Assert.NotNull(result);
//            Assert.NotEmpty(result.Results);
//            Assert.Equal(GetFakeReturnDetails().Count, result.Results.Count);

//            var distinctClusterIDs = result.Results.Select(r => r.ClusterID).Distinct().ToList();
//            Assert.Equal(3, distinctClusterIDs.Count);

//            Assert.InRange(result.QualityScore, 0.0, 1.0);
//            Assert.NotNull(result.ClusterKeywords);
//            Assert.Equal(distinctClusterIDs.Count, result.ClusterKeywords.Count);

//            Assert.NotNull(result.ClusterMetrics);
//            Assert.Single(result.ClusterMetrics);
//            Assert.True(result.ClusterMetrics.ContainsKey(config.ClusterCount));
//            Assert.Equal(result.QualityScore, result.ClusterMetrics[config.ClusterCount], precision: 5);
//        }

//        [Fact]
//        public async Task AnalyzeReturnReasonsAsync_WithEmptyData_ReturnsEmptyResults()
//        {
//            var mockDao = new Mock<ReturnDAO>();
//            mockDao.Setup(d => d.Select()).Returns(new List<ReturnDetailDTO>());

//            var config = CreateTestConfig();
//            var bll = new ReturnAnalysisBLL(mockDao.Object);

//            var result = await bll.AnalyzeReturnReasonsAsync(config, CancellationToken.None);

//            Assert.NotNull(result);
//            Assert.Empty(result.Results);
//            Assert.Empty(result.ClusterKeywords);
//            Assert.Equal(0, result.QualityScore);

//            if (result.ClusterMetrics.Any())
//            {
//                Assert.Single(result.ClusterMetrics);
//                Assert.True(result.ClusterMetrics.ContainsKey(config.ClusterCount));
//                Assert.Equal(0, result.ClusterMetrics[config.ClusterCount]);
//            }
//        }

//        [Fact]
//        public async Task FindOptimalClusterCountAsync_ForThreeThemeData_SuggestsThreeClusters()
//        {
//            var mockDao = new Mock<ReturnDAO>();
//            var bll = new ReturnAnalysisBLL(mockDao.Object);

//            var rawDetails = GetFakeReturnDetails();
//            var stopWords = new HashSet<string> { "on", "the", "for", "and", "was", "not", "too", "as", "compared", "my", "me", "to" };
//            var preprocessConfig = CreateTestConfig(clusterCount: 3, stopWords: stopWords);
//            var preprocessed = await bll.ProcessReturnReasonsAsync(rawDetails, preprocessConfig, CancellationToken.None);

//            Assert.NotEmpty(preprocessed);

//            int maxClusters = 5;
//            var (optimalK, metrics) = await bll.FindOptimalClusterCountAsync(preprocessed, maxClusters, CancellationToken.None);

//            Assert.True(optimalK >= 2 && optimalK <= maxClusters);
//            Assert.Equal(3, optimalK);

//            Assert.NotNull(metrics);
//            Assert.NotEmpty(metrics);
//            Assert.True(metrics.ContainsKey(optimalK));

//            double bestScore = metrics[optimalK];
//            foreach (var score in metrics.Values)
//            {
//                Assert.True(bestScore >= score);
//            }

//            Assert.Contains(2, metrics.Keys);
//            Assert.Contains(3, metrics.Keys);
//        }

//        [Fact]
//        public async Task ProcessReturnReasonsAsync_CorrectlyProcessesReasons()
//        {
//            var bll = new ReturnAnalysisBLL(new Mock<ReturnDAO>().Object);
//            var returns = new List<ReturnDetailDTO>
//            {
//                new ReturnDetailDTO { ReturnID = 1, ReturnReason = "  Product! Was,,. Damaged.  " }
//            };
//            var stopWords = new HashSet<string> { "was" };
//            var config = CreateTestConfig(clusterCount: 1, stopWords: stopWords);

//            var processed = await bll.ProcessReturnReasonsAsync(returns, config, CancellationToken.None);

//            Assert.Single(processed);
//            Assert.Equal(1, processed[0].ReturnID);
//            Assert.Equal("product damaged", processed[0].Reason);
//            Assert.Equal("  Product! Was,,. Damaged.  ", processed[0].OriginalReason);
//        }
//    }
//}
