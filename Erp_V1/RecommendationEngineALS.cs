using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public class RecommendationEngineALS
    {
        private SalesBLL salesBLL = new SalesBLL();
        private ProductBLL productBLL = new ProductBLL();
        private CustomerBLL customerBLL = new CustomerBLL();

        public List<ProductRecommendationDTO> GetRecommendationsForCustomer(string customerName)
        {
            var allSales = salesBLL.Select().Sales;
            var allProducts = productBLL.Select().Products;
            var customer = customerBLL.Select().Customers.FirstOrDefault(c => c.CustomerName == customerName);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            var customerSales = allSales.Where(s => s.CustomerID == customer.ID).ToList();

            if (!customerSales.Any())
            {
                // No sales history, return a message or an empty list
                throw new Exception($"No recommendations found for {customerName} due to lack of purchase history.");
            }

            // Create the user-item matrix
            var userItemMatrix = CreateUserItemMatrix(allSales, allProducts);

            // Apply ALS to the matrix
            var alsModel = new ALSModel(userItemMatrix, 10, 0.1, 10);  // Parameters can be tuned for better performance
            var userFactors = alsModel.UserFactors;
            var productFactors = alsModel.ProductFactors;

            // Generate recommendations based on the factorized matrix
            var recommendations = RecommendProducts(userFactors, productFactors, customer.ID, allProducts);

            return recommendations;
        }

        private double[,] CreateUserItemMatrix(List<SalesDetailDTO> allSales, List<ProductDetailDTO> allProducts)
        {
            var userIds = allSales.Select(s => s.CustomerID).Distinct().ToArray();
            var productIds = allProducts.Select(p => p.ProductID).Distinct().ToArray();

            var userIndex = userIds.ToDictionary(id => id, idx => Array.IndexOf(userIds, idx));
            var productIndex = productIds.ToDictionary(id => id, idx => Array.IndexOf(productIds, idx));

            var matrix = new double[userIds.Length, productIds.Length];

            foreach (var sale in allSales)
            {
                var userId = sale.CustomerID;
                var productId = sale.ProductID;
                var userRow = userIndex[userId];
                var productCol = productIndex[productId];

                matrix[userRow, productCol] = sale.Price;  // Adjust this if using a different metric (e.g., rating, quantity)
            }

            return matrix;
        }

        private List<ProductRecommendationDTO> RecommendProducts(
            Matrix<double> userFactors,
            Matrix<double> productFactors,
            int customerId,
            List<ProductDetailDTO> allProducts)
        {
            // Get predicted ratings for the user
            int userIndex = customerId;
            var predictedRatings = userFactors.Row(userIndex) * productFactors.Transpose();

            var recommendations = new List<ProductRecommendationDTO>();

            for (int i = 0; i < predictedRatings.Count; i++)
            {
                if (predictedRatings[i] > 0)
                {
                    var product = allProducts.FirstOrDefault(p => p.ProductID == i);
                    if (product != null)
                    {
                        recommendations.Add(new ProductRecommendationDTO
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            CategoryName = product.CategoryName,
                            Price = product.price,
                            PredictedRating = predictedRatings[i]
                        });
                    }
                }
            }

            return recommendations.OrderByDescending(r => r.PredictedRating).ToList();
        }
    }
}
