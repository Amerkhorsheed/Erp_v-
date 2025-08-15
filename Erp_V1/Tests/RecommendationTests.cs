using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning;
using Accord.Math.Distances;
using Erp_V1.BLL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Erp_V1.Tests
{
    /// <summary>
    /// Contains unit tests for <see cref="RecommendationBLL"/>, 
    /// verifying both clustering diagnostics and end-to-end recommendation logic.
    /// </summary>
    [TestClass]
    public class RecommendationTests
    {
        private RecommendationBLL _recommendationBll;
        private Mock<SalesDAO> _salesDaoMock;
        private Mock<ProductDAO> _productDaoMock;

        // Generated test data:
        private List<SalesDetailDTO> _salesDataset;
        private List<ProductDetailDTO> _productCatalog;

        // Constants for dataset dimensions
        private const int CustomerCount = 100;
        private const int ProductCount = 50;
        private const int CategoryCount = 5;
        private const int SalesCount = 5000;
        private static readonly Random _random = new Random(0); // Fixed seed for reproducibility

        #region Test Initialization

        /// <summary>
        /// Runs before each test. Generates a large, semi-structured dataset
        /// and configures the DAO mocks to return that data.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Generate a realistic set of products and sales.
            (_salesDataset, _productCatalog) = GenerateRealisticSalesData(
                customerCount: CustomerCount,
                productCount: ProductCount,
                categoryCount: CategoryCount,
                salesCount: SalesCount);

            // Create and configure Moq for SalesDAO and ProductDAO
            _salesDaoMock = new Mock<SalesDAO>();
            _productDaoMock = new Mock<ProductDAO>();

            // When Select() is called, return our generated lists
            _salesDaoMock.Setup(s => s.Select()).Returns(_salesDataset);
            _productDaoMock.Setup(p => p.Select()).Returns(_productCatalog);

            // Instantiate RecommendationBLL with these mocks and a null logger
            _recommendationBll = new RecommendationBLL(
                _salesDaoMock.Object,
                _productDaoMock.Object,
                NullLogger<RecommendationBLL>.Instance
            );
        }

        #endregion

        #region Clustering Diagnostics Tests

        /// <summary>
        /// Verifies that <see cref="RecommendationBLL.EvaluateClusters(double[][], int)"/>
        /// returns non-empty silhouette diagnostics for a valid feature matrix.
        /// </summary>
        [TestMethod]
        public void EvaluateClusters_ReturnsNonEmptyDiagnostics()
        {
            // Arrange: Build a feature matrix from (Price, CategoryID) for each sale
            // We collapse by customer: for clustering, we need one row per customer,
            // so take the average Price and most-common CategoryID per customer for simplicity.
            var customerGroups = _salesDataset
                .GroupBy(s => s.CustomerID)
                .ToList();

            var features = customerGroups
                .Select(g =>
                {
                    double avgPrice = g.Average(s => s.Price);
                    int modalCategory = g
                        .GroupBy(s => s.CategoryID)
                        .OrderByDescending(grp => grp.Count())
                        .First().Key;
                    return new double[] { avgPrice, modalCategory };
                })
                .ToArray();

            const int maxK = 10;

            // Act
            List<ClusteringDiagnostics> diagnostics = _recommendationBll.EvaluateClusters(features, maxK);

            // Assert
            Assert.IsNotNull(diagnostics, "Diagnostics list should not be null.");
            Assert.IsTrue(
                diagnostics.Count >= 1,
                "Diagnostics should contain at least one entry when data is sufficient."
            );

            // Output for visual inspection (non-failing)
            Console.WriteLine("---- Silhouette Scores by K ----");
            Console.WriteLine("  K  |  Silhouette Score");
            Console.WriteLine("-----|--------------------");
            foreach (var diag in diagnostics.OrderBy(d => d.K))
            {
                Console.WriteLine($"  {diag.K,2} |       {diag.SilhouetteScore:F4}");
            }
            Console.WriteLine("--------------------------------");

            // Verify that the best silhouette is above a low threshold (0.1)
            double bestScore = diagnostics.Max(d => d.SilhouetteScore);
            Assert.IsTrue(
                bestScore > 0.1,
                $"Expected a best silhouette score > 0.1, but got {bestScore:F4}. Possible data issue."
            );
        }

        /// <summary>
        /// Verifies that <see cref="RecommendationBLL.DetermineOptimalK(double[][])"/>
        /// returns the same K as the diagnostic with the highest silhouette score.
        /// </summary>
        [TestMethod]
        public void DetermineOptimalK_ReturnsCorrectK()
        {
            // Arrange: same feature matrix as in EvaluateClusters test
            var customerGroups = _salesDataset
                .GroupBy(s => s.CustomerID)
                .ToList();

            var features = customerGroups
                .Select(g =>
                {
                    double avgPrice = g.Average(s => s.Price);
                    int modalCategory = g
                        .GroupBy(s => s.CategoryID)
                        .OrderByDescending(grp => grp.Count())
                        .First().Key;
                    return new double[] { avgPrice, modalCategory };
                })
                .ToArray();

            // Act
            int optimalK = _recommendationBll.DetermineOptimalK(features);

            // Recompute diagnostics to find expected K
            var diagnostics = _recommendationBll.EvaluateClusters(features, maxK: 10);
            int expectedK = diagnostics
                .OrderByDescending(d => d.SilhouetteScore)
                .First()
                .K;

            // Assert
            Assert.AreEqual(
                expectedK,
                optimalK,
                $"DetermineOptimalK() returned {optimalK}, but expected {expectedK} for highest silhouette."
            );
        }

        #endregion

        #region Recommendation Logic Tests

        /// <summary>
        /// Verifies that <see cref="RecommendationBLL.GetRecommendationsForCustomer(string)"/>
        /// returns a non-empty recommendation list for a known customer.
        /// Also verifies that no recommended product has already been purchased by that customer.
        /// </summary>
        [TestMethod]
        public void GetRecommendationsForCustomer_KnownCustomer_ReturnsValidRecommendations()
        {
            // Arrange: pick a random existing customer
            var sampleCustomer = _salesDataset
                .Select(s => new { s.CustomerID, s.CustomerName })
                .First();

            string customerName = sampleCustomer.CustomerName;

            // Act
            List<ProductDetailDTO> recommendations =
                _recommendationBll.GetRecommendationsForCustomer(customerName);

            // Assert: Recommendations should not be null
            Assert.IsNotNull(
                recommendations,
                "Recommendations should not be null for an existing customer."
            );

            // Recommendations should be at most DefaultRecommendationCount (5 by default)
            Assert.IsTrue(
                recommendations.Count <= 5,
                $"Expected at most 5 recommendations, but got {recommendations.Count}."
            );

            // No recommended ProductID should appear in that customer's purchase history
            var purchasedProductIds = _salesDataset
                .Where(s => s.CustomerName == customerName)
                .Select(s => s.ProductID)
                .ToHashSet();

            bool anyOverlap = recommendations.Any(r => purchasedProductIds.Contains(r.ProductID));
            Assert.IsFalse(
                anyOverlap,
                "Recommendations should not include any products that the customer has already purchased."
            );
        }

        /// <summary>
        /// Verifies that <see cref="RecommendationBLL.GetRecommendationsForCustomer(string)"/>
        /// falls back to top-selling products if the requested customer does not exist.
        /// </summary>
        [TestMethod]
        public void GetRecommendationsForCustomer_UnknownCustomer_ReturnsTopSellers()
        {
            // Arrange: pick a name not in the dataset
            string fakeCustomer = "Customer-DoesNotExist";

            // Act
            List<ProductDetailDTO> recommendations =
                _recommendationBll.GetRecommendationsForCustomer(fakeCustomer);

            // Assert: Recommendations should still be returned (fallback to top sellers)
            Assert.IsNotNull(
                recommendations,
                "Recommendations should not be null even for unknown customer."
            );
            Assert.IsTrue(
                recommendations.Count > 0,
                "Expected at least one top-selling recommendation as fallback."
            );

            // Verify that each recommended product appears in the product catalog
            var allProductIds = _productCatalog.Select(p => p.ProductID).ToHashSet();
            foreach (var rec in recommendations)
            {
                Assert.IsTrue(
                    allProductIds.Contains(rec.ProductID),
                    $"ProductID {rec.ProductID} in recommendations is not in the product catalog."
                );
            }
        }

        /// <summary>
        /// Verifies that <see cref="RecommendationBLL.GetRecommendationsForCustomer(string)"/>
        /// handles a customer with exactly one purchase gracefully by returning top-sellers fallback.
        /// </summary>
        [TestMethod]
        public void GetRecommendationsForCustomer_SinglePurchaseCustomer_ReturnsTopSellers()
        {
            // Arrange: create a temporary dataset where one customer has only one sale
            var singlePurchaseCustomer = new SalesDetailDTO
            {
                SalesID = _random.Next(1, int.MaxValue),
                CustomerID = 999,
                CustomerName = "Single-Purchase",
                ProductID = _productCatalog.First().ProductID,
                ProductName = _productCatalog.First().ProductName,
                CategoryID = _productCatalog.First().CategoryID,
                CategoryName = _productCatalog.First().CategoryName,
                Price = (int)_productCatalog.First().price,
                SalesAmount = 1,
                SalesDate = DateTime.Today,
                StockAmount = _productCatalog.First().stockAmount,
                isCategoryDeleted = false,
                isProductDeleted = false,
                isCustomerDeleted = false,
                MinQty = _productCatalog.First().MinQty,
                MaxDiscount = _productCatalog.First().MaxDiscount,
                Madfou3 = 0,
                Baky = 0
            };
            _salesDataset.Add(singlePurchaseCustomer);
            _salesDaoMock.Setup(s => s.Select()).Returns(_salesDataset);

            // Act
            List<ProductDetailDTO> recommendations =
                _recommendationBll.GetRecommendationsForCustomer("Single-Purchase");

            // Assert: Since there are too few data points to form a cluster, fallback to top-sellers
            Assert.IsNotNull(
                recommendations,
                "Recommendations should not be null for single-purchase customer."
            );
            Assert.IsTrue(
                recommendations.Count > 0,
                "Expected at least one top-selling recommendation as fallback for single-purchase customer."
            );

            // Ensure that the single-purchase product is not in the returned list
            var forbiddenProductId = singlePurchaseCustomer.ProductID;
            Assert.IsFalse(
                recommendations.Any(r => r.ProductID == forbiddenProductId),
                "Recommendations should not include the already-purchased product for single-purchase customer."
            );
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Generates a large, semi-structured dataset to simulate realistic sales patterns.
        /// It creates artificial “customer personas” that yield natural clusters when using (avg Price, modal CategoryID).
        /// </summary>
        /// <param name="customerCount">Number of distinct customers to generate.</param>
        /// <param name="productCount">Number of distinct products to generate.</param>
        /// <param name="categoryCount">Number of distinct categories to generate.</param>
        /// <param name="salesCount">Total number of sales transactions to generate.</param>
        /// <returns>
        /// A tuple containing:
        ///   1. A <see cref="List{SalesDetailDTO}"/> of generated sales.
        ///   2. A <see cref="List{ProductDetailDTO}"/> of generated products.
        /// </returns>
        private (List<SalesDetailDTO> Sales, List<ProductDetailDTO> Products)
            GenerateRealisticSalesData(int customerCount, int productCount, int categoryCount, int salesCount)
        {
            var products = new List<ProductDetailDTO>(capacity: productCount);
            var sales = new List<SalesDetailDTO>(capacity: salesCount);

            // 1) Build product catalog
            for (int pid = 1; pid <= productCount; pid++)
            {
                int categoryId = _random.Next(1, categoryCount + 1);
                var product = new ProductDetailDTO
                {
                    ProductID = pid,
                    ProductName = $"Product-{pid}",
                    CategoryID = categoryId,
                    CategoryName = $"Category-{categoryId}",
                    price = _random.Next(10, 501),      // $10–$500
                    stockAmount = _random.Next(1, 100),
                    Sale_Price = 0,
                    MinQty = 1,
                    MaxDiscount = 0
                };
                products.Add(product);
            }

            // Precompute “customer personas” to simulate clusterable behavior
            var personaDelegates = new List<Action<int>>();
            personaDelegates.Add(customerId =>
            {
                // Persona A: Buys “cheap” products in low-index categories
                var candidates = products
                    .Where(p => p.price < 150 && p.CategoryID <= categoryCount / 2)
                    .ToArray();
                var chosen = candidates[_random.Next(candidates.Length)];
                sales.Add(CreateSale(customerId, chosen));
            });
            personaDelegates.Add(customerId =>
            {
                // Persona B: Buys “expensive” products in high-index categories
                var candidates = products
                    .Where(p => p.price > 350 && p.CategoryID > categoryCount / 2)
                    .ToArray();
                var chosen = candidates[_random.Next(candidates.Length)];
                sales.Add(CreateSale(customerId, chosen));
            });
            personaDelegates.Add(customerId =>
            {
                // Persona C: Buys mid-range products across all categories
                var candidates = products
                    .Where(p => p.price >= 150 && p.price <= 350)
                    .ToArray();
                var chosen = candidates[_random.Next(candidates.Length)];
                sales.Add(CreateSale(customerId, chosen));
            });

            // 2) Generate random salesCount transactions
            for (int i = 0; i < salesCount; i++)
            {
                int customerId = _random.Next(1, customerCount + 1);
                var persona = personaDelegates[(customerId - 1) % personaDelegates.Count];
                persona(customerId);
            }

            return (sales, products);
        }

        /// <summary>
        /// Helper to construct a <see cref="SalesDetailDTO"/> for a given customer &amp; product.
        /// </summary>
        private SalesDetailDTO CreateSale(int customerId, ProductDetailDTO product)
        {
            return new SalesDetailDTO
            {
                SalesID = _random.Next(1, int.MaxValue),
                CustomerID = customerId,
                CustomerName = $"Customer-{customerId}",
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                CategoryName = product.CategoryName,
                Price = product.price,
                SalesAmount = _random.Next(1, 5),      // quantity 1–4
                SalesDate = DateTime.Today.AddDays(-_random.Next(0, 365)),
                StockAmount = product.stockAmount,
                isCategoryDeleted = false,
                isProductDeleted = false,
                isCustomerDeleted = false,
                MinQty = product.MinQty,
                MaxDiscount = product.MaxDiscount,
                Madfou3 = 0,
                Baky = 0
            };
        }

        #endregion
    }
}
