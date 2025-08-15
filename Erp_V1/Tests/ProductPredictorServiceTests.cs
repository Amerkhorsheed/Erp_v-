using Microsoft.VisualStudio.TestTools.UnitTesting;
using Erp_V1.BLL;
using System.Collections.Generic;
using Erp_V1.DAL.DTO;
using Moq; // Assuming you are using Moq for mocking. If not, you'll need to use your preferred mocking framework or create manual mock implementations.
using Erp_V1.DAL.DAO;
using System.Linq;
using System;

namespace Erp_V1.Tests
{
    [TestClass]
    public class ProductPredictorServiceTests
    {
        private Mock<SalesDAO> _mockSalesDao;
        private Mock<ProductDAO> _mockProductDao;
        private ProductPredictorService _predictorService;

        [TestInitialize]
        public void Setup()
        {
            // Initialize mock DAOs before each test.
            _mockSalesDao = new Mock<SalesDAO>();
            _mockProductDao = new Mock<ProductDAO>();

            // Assume ProductPredictorService has a constructor that accepts SalesDAO and ProductDAO.
            // Modify ProductPredictorService.cs if this constructor doesn't exist.
            _predictorService = new ProductPredictorService(_mockSalesDao.Object, _mockProductDao.Object);
        }

        [TestMethod]
        public void GenerateProductForecasts_ValidParameters_ReturnsPredictions()
        {
            // Arrange: Create a valid set of parameters.
            var parameters = new PredictionParameters
            {
                ForecastHorizon = 30,
                ConfidenceLevel = 95,
                MinimumDataPoints = 30,
                SeasonalityPeriod = 7,
                TrendWindow = 30
            };

            // Create a sample active product.
            var product = new ProductDetailDTO { ProductID = 1, ProductName = "Test Product", isCategoryDeleted = false };
            _mockProductDao.Setup(dao => dao.Select()).Returns(new List<ProductDetailDTO> { product });

            // Create a list of at least 30 sales records for the test product, with at least one in the past year.
            var salesData = new List<SalesDetailDTO>();
            DateTime today = DateTime.Today;
            for (int i = 0; i < 29; i++)
            {
                salesData.Add(new SalesDetailDTO
                {
                    ProductID = product.ProductID,
                    SalesDate = today.AddDays(-i - 30),
                    SalesAmount = 1,
                    Price = 10
                });
            }
            // Add a sale within the last year.
            salesData.Add(new SalesDetailDTO
            {
                ProductID = product.ProductID,
                SalesDate = today.AddDays(-5),
                SalesAmount = 2,
                Price = 12
            });

            _mockSalesDao.Setup(dao => dao.Select()).Returns(salesData);

            // Act: Generate forecasts.
            var predictions = _predictorService.GenerateProductForecasts(parameters);

            // Assert: Ensure that predictions are returned for at least one product.
            Assert.IsNotNull(predictions);
            Assert.IsTrue(predictions.Any(), "No predictions were generated for valid parameters.");
            Assert.AreEqual(1, predictions.Count, "Expected a prediction for one product.");
            Assert.AreEqual(product.ProductID, predictions.First().ProductID, "Prediction should be for the test product.");
            Assert.IsNotNull(predictions.First().PredictedSales, "Predicted sales should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateProductForecasts_InvalidForecastHorizon_ThrowsArgumentException()
        {
            // Arrange: Create parameters with an invalid forecast horizon.
            var parameters = new PredictionParameters
            {
                ForecastHorizon = 5,  // invalid since it should be between 7 and 365 (based on common forecasting practices)
                ConfidenceLevel = 95,
                MinimumDataPoints = 30,
                SeasonalityPeriod = 7,
                TrendWindow = 30
            };

            // Act: This should throw an exception.
            _predictorService.GenerateProductForecasts(parameters);
        }

        // You can add more test methods to cover other scenarios, such as:
        // - Testing with insufficient data (to ensure no predictions are generated).
        // - Testing with different parameter values.
        // - Testing the behavior when there are no active products.
        // - Testing the handling of edge cases or invalid data.
    }
}