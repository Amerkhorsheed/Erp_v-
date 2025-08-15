using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Accord.MachineLearning;
using Accord.Math.Distances;
using Erp_V1.BLL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Microsoft.Extensions.Logging.Abstractions;

namespace Erp_V1.Tests.BLL
{
    /// <summary>
    /// Advanced unit tests for the <see cref="RecommendationBLL"/> class.
    /// These tests generate large synthetic datasets with known clustering structure
    /// and verify that DetermineOptimalK returns a reasonable cluster count.
    /// Additionally, silhouette scores for K = 2..5 are computed and logged.
    /// Finally, we validate recommendation logic on large-scale sales/product catalogs,
    /// ensuring only already-purchased items are excluded and the total recommendations match expectations.
    /// </summary>
    [TestClass]
    public class RecommendationBLLAdvancedTests
    {
        private const int PointsPerCluster = 500;
        private const int MaxClustersToTest = 5;
        private const int CenterVariance = 10; // Integer spread around each cluster center

        private RecommendationBLL _recommendationBLL;
        private Mock<SalesDAO> _salesDaoMock;
        private Mock<ProductDAO> _productDaoMock;

        #region Test Data Generators

        /// <summary>
        /// Generates synthetic sales data forming <paramref name="numClusters"/> distinct clusters.
        /// Each cluster has <paramref name="PointsPerCluster"/> points.
        /// Cluster centers are placed at (Price, CategoryID) = (i * 1000, i) for i = 1..numClusters.
        /// </summary>
        /// <param name="numClusters">Number of clusters to generate.</param>
        /// <returns>List of <see cref="SalesDetailDTO"/> covering all clusters.</returns>
        private static List<SalesDetailDTO> GenerateClusteredSalesData(int numClusters)
        {
            var rnd = new Random(42);
            var sales = new List<SalesDetailDTO>();
            int salesIdCounter = 1;
            int customerBaseId = 1000;

            for (int clusterIndex = 1; clusterIndex <= numClusters; clusterIndex++)
            {
                int centerPrice = clusterIndex * 1000;
                int categoryId = clusterIndex; // distinct category per cluster

                for (int i = 0; i < PointsPerCluster; i++)
                {
                    // Price is centered around centerPrice with small integer noise.
                    int noise = rnd.Next(-CenterVariance / 2, CenterVariance / 2 + 1);
                    int price = centerPrice + noise;

                    sales.Add(new SalesDetailDTO
                    {
                        SalesID = salesIdCounter++,
                        ProductID = (clusterIndex * 10000) + i,
                        CustomerID = customerBaseId + clusterIndex,
                        CustomerName = $"ClusterCustomer{clusterIndex}",
                        Price = price,
                        CategoryID = categoryId
                    });
                }
            }

            // Shuffle for randomness
            return sales.OrderBy(_ => rnd.Next()).ToList();
        }

        /// <summary>
        /// Generates a product catalog that includes at least all product IDs found in <paramref name="salesData"/>.
        /// Additionally, adds some extra products to test exclusion of already-purchased items.
        /// </summary>
        /// <param name="salesData">List of <see cref="SalesDetailDTO"/> representing purchased items.</param>
        /// <returns>List of <see cref="ProductDetailDTO"/> including purchased and extra items.</returns>
        private static List<ProductDetailDTO> GenerateProductCatalog(List<SalesDetailDTO> salesData)
        {
            var products = new List<ProductDetailDTO>();
            var distinctProductIds = salesData.Select(s => s.ProductID).Distinct().ToList();

            // Include all purchased products in catalog
            foreach (var pid in distinctProductIds)
            {
                var sale = salesData.First(s => s.ProductID == pid);
                products.Add(new ProductDetailDTO
                {
                    ProductID = pid,
                    CategoryID = sale.CategoryID,
                    ProductName = $"PurchasedProduct_{pid}",
                    price = sale.Price
                });
            }

            // Add extra products in each category up to 10 per category
            var rnd = new Random(99);
            var existingCategoryIds = distinctProductIds
                .Select(pid => salesData.First(s => s.ProductID == pid).CategoryID)
                .Distinct()
                .ToList();

            foreach (var categoryId in existingCategoryIds)
            {
                for (int i = 1; i <= 10; i++)
                {
                    int newProductId = (categoryId * 100000) + i;
                    int randomPrice = categoryId * 1000 + rnd.Next(0, 100);
                    products.Add(new ProductDetailDTO
                    {
                        ProductID = newProductId,
                        CategoryID = categoryId,
                        ProductName = $"ExtraProduct_Cat{categoryId}_{i}",
                        price = randomPrice
                    });
                }
            }

            return products;
        }

        #endregion

        #region Setup

        /// <summary>
        /// Initializes mocks before each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _salesDaoMock = new Mock<SalesDAO>();
            _productDaoMock = new Mock<ProductDAO>();
            // _recommendationBLL will be instantiated in each test since it depends on generated data
        }

        #endregion

        #region DetermineOptimalK Tests

        /// <summary>
        /// Tests DetermineOptimalK for synthetic data with cluster counts from 1 to <see cref="MaxClustersToTest"/>.
        /// Verifies that the returned optimal K is reasonable (>= 1 and <= MaxClustersToTest).
        /// Also computes and logs silhouette scores for K = 2..<see cref="MaxClustersToTest"/> for deeper insight.
        /// </summary>
        [TestMethod]
        public void DetermineOptimalK_K1ToK5_ReturnsReasonableK_WithSilhouetteScores()
        {
            for (int expectedClusters = 1; expectedClusters <= MaxClustersToTest; expectedClusters++)
            {
                // Arrange: generate synthetic sales data with 'expectedClusters' clusters
                var salesData = GenerateClusteredSalesData(expectedClusters);
                _salesDaoMock.Setup(dao => dao.Select()).Returns(salesData);
                _productDaoMock.Setup(dao => dao.Select()).Returns(new List<ProductDetailDTO>());

                // Build feature array [Price, CategoryID]
                var featureArray = salesData
                    .Select(s => new double[] { s.Price, s.CategoryID })
                    .ToArray();

                _recommendationBLL = new RecommendationBLL(
                    _salesDaoMock.Object,
                    _productDaoMock.Object,
                    NullLogger<RecommendationBLL>.Instance
                );

                // Act: call DetermineOptimalK directly
                int actualOptimalK = _recommendationBLL.DetermineOptimalK(featureArray);

                // Log silhouette scores for K = 2..MaxClustersToTest (if enough data points)
                if (featureArray.Length >= 2)
                {
                    Console.WriteLine($"--- Silhouette scores for dataset with {expectedClusters} true cluster(s) ---");
                    for (int k = 2; k <= MaxClustersToTest && k < featureArray.Length; k++)
                    {
                        try
                        {
                            var kmeans = new KMeans(k, new SquareEuclidean())
                            {
                                Tolerance = 0.05,
                                MaxIterations = 100
                            };
                            var clusters = kmeans.Learn(featureArray);
                            int[] labels = featureArray.Select(x => clusters.Decide(x)).ToArray();

                            double silhouetteScore = ComputeSilhouetteScore(featureArray, labels);
                            Console.WriteLine($"  K = {k}, Silhouette Score = {silhouetteScore:F4}");
                        }
                        catch
                        {
                            Console.WriteLine($"  K = {k}, Silhouette Score = N/A (insufficient data or error)");
                        }
                    }
                    Console.WriteLine($"--- End silhouette scores for expectedClusters = {expectedClusters} ---");
                }

                // Log the determined K
                Console.WriteLine($"Expected up to {expectedClusters} cluster(s), Determined K: {actualOptimalK}");

                // Assert: actualOptimalK is at least 1 and at most MaxClustersToTest
                Assert.IsTrue(
                    actualOptimalK >= 1 && actualOptimalK <= MaxClustersToTest,
                    $"DetermineOptimalK returned {actualOptimalK}, which is outside the range [1, {MaxClustersToTest}]."
                );
            }
        }

        #endregion

        #region Large-Scale Recommendation Tests

        /// <summary>
        /// Tests GetRecommendationsForCustomer with a large sales dataset containing 5 clusters of 500 points each.
        /// Verifies that:
        /// 1. DetermineOptimalK does not throw.
        /// 2. Recommendations exclude already purchased products.
        /// 3. The total number of recommendations equals the catalog size minus purchased count.
        /// </summary>
        [TestMethod]
        public void GetRecommendationsForCustomer_WithLargeSyntheticData_WorksCorrectly()
        {
            const int totalClusters = MaxClustersToTest;
            const int testClusterIndex = 3; // Arbitrarily pick cluster #3 for testing recommendation

            // Arrange: generate synthetic sales data with 5 clusters, each of 500 points
            var largeSalesData = GenerateClusteredSalesData(totalClusters);
            _salesDaoMock.Setup(dao => dao.Select()).Returns(largeSalesData);

            // Generate a product catalog including all purchased + extras
            var productCatalog = GenerateProductCatalog(largeSalesData);
            _productDaoMock.Setup(dao => dao.Select()).Returns(productCatalog);

            _recommendationBLL = new RecommendationBLL(
                _salesDaoMock.Object,
                _productDaoMock.Object,
                NullLogger<RecommendationBLL>.Instance
            );

            // Identify the test customer from the chosen cluster
            string testCustomerName = $"ClusterCustomer{testClusterIndex}";
            var purchasedByTestCustomer = largeSalesData
                .Where(s => s.CustomerName.Equals(testCustomerName, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.ProductID)
                .ToHashSet();

            // Act: request recommendations
            var recommendations = _recommendationBLL.GetRecommendationsForCustomer(testCustomerName);

            // Log basic metrics
            Console.WriteLine($"Total sales records: {largeSalesData.Count}");
            Console.WriteLine($"Total product catalog size: {productCatalog.Count}");
            Console.WriteLine($"Number of recommendations returned: {recommendations.Count}");

            // Assert: recommendations list is not null
            Assert.IsNotNull(
                recommendations,
                "Recommendations list should not be null for a customer with purchase history."
            );

            // Assert: number of recommendations equals catalog size minus purchased count
            int expectedRecommendationCount = productCatalog.Count - purchasedByTestCustomer.Count;
            Assert.AreEqual(
                expectedRecommendationCount,
                recommendations.Count,
                $"Expected {expectedRecommendationCount} recommendations but got {recommendations.Count}."
            );

            // Assert: no recommended product was already purchased
            foreach (var recommendedProduct in recommendations)
            {
                Assert.IsFalse(
                    purchasedByTestCustomer.Contains(recommendedProduct.ProductID),
                    $"Recommended product ID {recommendedProduct.ProductID} was already purchased by {testCustomerName}."
                );
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Computes the average silhouette score for the given dataset and cluster labels.
        /// </summary>
        /// <param name="data">2D array where each row is a data point [Price, CategoryID].</param>
        /// <param name="labels">Cluster label for each data point.</param>
        /// <returns>Average silhouette coefficient across all data points.</returns>
        private static double ComputeSilhouetteScore(double[][] data, int[] labels)
        {
            int n = data.Length;
            var distance = new SquareEuclidean();
            double totalSilhouette = 0.0;

            // Precompute distances
            double[,] distMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    double d = distance.Distance(data[i], data[j]);
                    distMatrix[i, j] = d;
                    distMatrix[j, i] = d;
                }
            }

            // For each point, compute a(i) and b(i)
            for (int i = 0; i < n; i++)
            {
                int labelI = labels[i];
                // Distances to points in same cluster
                var sameClusterDistances = new List<double>();
                // Distances to points in other clusters, grouped by cluster
                var otherClusterDistances = new Dictionary<int, List<double>>();

                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    int labelJ = labels[j];
                    double d = distMatrix[i, j];
                    if (labelJ == labelI)
                        sameClusterDistances.Add(d);
                    else
                    {
                        if (!otherClusterDistances.ContainsKey(labelJ))
                            otherClusterDistances[labelJ] = new List<double>();
                        otherClusterDistances[labelJ].Add(d);
                    }
                }

                double a = sameClusterDistances.Any()
                    ? sameClusterDistances.Average()
                    : 0.0;

                double b = otherClusterDistances.Any()
                    ? otherClusterDistances.Values.Min(list => list.Average())
                    : 0.0;

                double s;
                if (Math.Max(a, b) > 0)
                    s = (b - a) / Math.Max(a, b);
                else
                    s = 0.0;

                totalSilhouette += s;
            }

            return totalSilhouette / n;
        }

        #endregion
    }
}
