using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Erp_V1.BLL
{
    public class LinearRegressionModel
    {
        private List<ProductDetailDTO> _trainingData;

        /// <summary>
        /// Trains the model using the provided list of products.
        /// </summary>
        /// <param name="products">List of ProductDetailDTO objects to be used as training data.</param>
        /// <exception cref="ArgumentException">Thrown if the products list is null, empty, or contains invalid entries.</exception>
        public void Train(IEnumerable<ProductDetailDTO> products)
        {
            if (products == null || !products.Any())
                throw new ArgumentException("Training data cannot be null or empty.");

            var validProducts = products.Where(p => p.ProductID > 0 && p.price > 0).ToList();

            if (!validProducts.Any())
                throw new ArgumentException("Training data must contain valid ProductID and Price values.");

            _trainingData = validProducts;
        }

        /// <summary>
        /// Predicts the stock amount for a given product based on training data.
        /// </summary>
        /// <param name="product">The ProductDetailDTO object for which to predict the stock amount.</param>
        /// <returns>Predicted stock amount as an integer.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the model has not been trained.</exception>
        /// <exception cref="ArgumentException">Thrown if the product is null or contains invalid data.</exception>
        public int Predict(ProductDetailDTO product)
        {
            ValidateModelState();
            ValidateProduct(product);

            var relatedProducts = _trainingData.Where(p => p.ProductID == product.ProductID).ToList();

            if (!relatedProducts.Any())
                throw new ArgumentException($"No training data found for ProductID: {product.ProductID}");

            double averageStockAmount = relatedProducts.Average(p => p.stockAmount);
            double predictedStockAmount = averageStockAmount + (product.price * 0.1);

            return Math.Max((int)predictedStockAmount, 0);
        }

        /// <summary>
        /// Ensures the model has been trained before performing predictions.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the model has not been trained.</exception>
        private void ValidateModelState()
        {
            if (_trainingData == null || !_trainingData.Any())
                throw new InvalidOperationException("Model has not been trained. Call Train() with valid data first.");
        }

        /// <summary>
        /// Validates the product to ensure it has valid properties for prediction.
        /// </summary>
        /// <param name="product">The ProductDetailDTO object to validate.</param>
        /// <exception cref="ArgumentException">Thrown if the product is null or contains invalid data.</exception>
        private static void ValidateProduct(ProductDetailDTO product)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null.");

            if (product.ProductID <= 0 || product.price <= 0)
                throw new ArgumentException($"Product data is invalid. ProductID: {product?.ProductID}, Price: {product?.price}");
        }
    }
}
