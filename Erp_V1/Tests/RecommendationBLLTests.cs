using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Erp_V1.DAL.DTO;
using Erp_V1.BLL;
using Erp_V1.DAL.DAO;
using Microsoft.Extensions.Logging.Abstractions;

namespace Erp_V1.Tests.BLL
{
    [TestClass]
    public class RecommendationBLLTests
    {
        private Mock<SalesDAO> _salesDaoMock;
        private Mock<ProductDAO> _productDaoMock;
        private RecommendationBLL _recommendationBLL;
        private List<SalesDetailDTO> _salesData;
        private List<ProductDetailDTO> _productData;

        [TestInitialize]
        public void Setup()
        {
            // Prepare sample sales data.
            _salesData = new List<SalesDetailDTO>
            {
                // Budget buyer sales.
                new SalesDetailDTO { CustomerID = 1, CustomerName = "BudgetBuyer", ProductID = 1, Price = 100, CategoryID = 1 },
                new SalesDetailDTO { CustomerID = 1, CustomerName = "BudgetBuyer", ProductID = 2, Price = 120, CategoryID = 1 },

                // Premium buyer sales.
                new SalesDetailDTO { CustomerID = 2, CustomerName = "PremiumBuyer", ProductID = 3, Price = 1500, CategoryID = 2 },
                new SalesDetailDTO { CustomerID = 2, CustomerName = "PremiumBuyer", ProductID = 4, Price = 1600, CategoryID = 2 },

                // Mixed buyer sales.
                new SalesDetailDTO { CustomerID = 3, CustomerName = "MixedBuyer", ProductID = 1, Price = 100, CategoryID = 1 },
                new SalesDetailDTO { CustomerID = 3, CustomerName = "MixedBuyer", ProductID = 3, Price = 1500, CategoryID = 2 }
            };

            // Prepare sample product catalog.
            _productData = new List<ProductDetailDTO>
            {
                new ProductDetailDTO { ProductID = 1, CategoryID = 1, ProductName = "Budget Product 1", price = 100 },
                new ProductDetailDTO { ProductID = 2, CategoryID = 1, ProductName = "Budget Product 2", price = 120 },
                new ProductDetailDTO { ProductID = 3, CategoryID = 2, ProductName = "Premium Product 1", price = 1500 },
                new ProductDetailDTO { ProductID = 4, CategoryID = 2, ProductName = "Premium Product 2", price = 1600 },
                new ProductDetailDTO { ProductID = 5, CategoryID = 3, ProductName = "Accessory Product", price = 50 },
            };

            // Setup mocks.
            _salesDaoMock = new Mock<SalesDAO>();
            _salesDaoMock.Setup(x => x.Select()).Returns(_salesData);

            _productDaoMock = new Mock<ProductDAO>();
            _productDaoMock.Setup(x => x.Select()).Returns(_productData);

            // Instantiate RecommendationBLL with mocked dependencies and a null logger.
            _recommendationBLL = new RecommendationBLL(
                _salesDaoMock.Object,
                _productDaoMock.Object,
                NullLogger<RecommendationBLL>.Instance
            );
        }

        [TestMethod]
        public void GetRecommendationsForCustomer_NoPurchaseHistory_ReturnsEmpty()
        {
            // Arrange
            string nonExistingCustomer = "NonExistentCustomer";

            // Act
            var recommendations = _recommendationBLL.GetRecommendationsForCustomer(nonExistingCustomer);

            // Assert
            Assert.IsNotNull(recommendations, "Recommendations should not be null.");
            Assert.AreEqual(0, recommendations.Count, "New customers should receive an empty recommendation list.");
        }

        [TestMethod]
        public void GetRecommendationsForCustomer_ExcludesPurchasedProducts()
        {
            // Arrange
            string customerName = "BudgetBuyer";

            // Act
            var recommendations = _recommendationBLL.GetRecommendationsForCustomer(customerName);

            // Assert
            // Get the list of product IDs already purchased by the customer.
            HashSet<int> purchasedProductIds = _salesData
                .Where(s => s.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.ProductID)
                .ToHashSet();

            // Verify that no recommended product was already purchased.
            foreach (var product in recommendations)
            {
                Assert.IsFalse(
                    purchasedProductIds.Contains(product.ProductID),
                    $"Product with ID {product.ProductID} was already purchased by {customerName}."
                );
            }
        }

        [TestMethod]
        public void GetRecommendationsForCustomer_ReturnsRecommendations_WhenDataExists()
        {
            // Arrange
            string customerName = "MixedBuyer";

            // Act
            var recommendations = _recommendationBLL.GetRecommendationsForCustomer(customerName);

            // Assert
            Assert.IsNotNull(recommendations, "Recommendations should not be null.");
            Assert.IsTrue(recommendations.Any(), "There should be recommendations for an existing customer.");
        }
    }
}
