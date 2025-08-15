using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;
using Accord.Math.Decompositions;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public class RecommendationEngineAI
    {
        private readonly SalesBLL _salesBLL;
        private readonly ProductBLL _productBLL;

        public RecommendationEngineAI(SalesBLL salesBLL, ProductBLL productBLL)
        {
            _salesBLL = salesBLL;
            _productBLL = productBLL;
        }

        public List<ProductRecommendationDTO> GetRecommendationsForCustomer(string customerName)
        {
            try
            {
                var allSales = _salesBLL.Select().Sales;
                var allProducts = _productBLL.Select().Products;

                var customerSales = allSales.Where(s => s.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase)).ToList();

                if (!customerSales.Any())
                {
                    throw new Exception($"No sales history found for customer: {customerName}.");
                }

                // Create the user-item matrix
                var userItemMatrix = CreateUserItemMatrix(allSales, allProducts);

                // Apply SVD
                var svd = new SingularValueDecomposition(userItemMatrix, false, true);
                var reconstructedMatrix = ReconstructMatrix(svd, userItemMatrix.GetLength(0), userItemMatrix.GetLength(1));

                // Generate recommendations based on reconstructed matrix
                var recommendations = RecommendProducts(reconstructedMatrix, customerSales.First().CustomerID, allProducts);

                if (!recommendations.Any())
                {
                    throw new Exception($"No product recommendations available for customer: {customerName}.");
                }

                return recommendations;
            }
            catch (Exception ex)
            {
                // Log the exception (assuming a logger is available)
                Console.WriteLine($"Error in GetRecommendationsForCustomer: {ex.Message}");
                throw;
            }
        }

        private double[,] CreateUserItemMatrix(List<SalesDetailDTO> allSales, List<ProductDetailDTO> allProducts)
        {
            var userIds = allSales.Select(s => s.CustomerID).Distinct().ToArray();
            var productIds = allProducts.Select(p => p.ProductID).Distinct().ToArray();

            var matrix = new double[userIds.Length, productIds.Length];
            foreach (var sale in allSales)
            {
                int userIndex = Array.IndexOf(userIds, sale.CustomerID);
                int productIndex = Array.IndexOf(productIds, sale.ProductID);
                if (userIndex >= 0 && productIndex >= 0)
                {
                    matrix[userIndex, productIndex] = sale.Price;
                }
            }

            return matrix;
        }

        private double[,] ReconstructMatrix(SingularValueDecomposition svd, int numRows, int numCols)
        {
            var u = svd.LeftSingularVectors;
            var s = svd.Diagonal;
            var v = svd.RightSingularVectors;

            var sMatrix = new double[s.Length, s.Length];
            for (int i = 0; i < s.Length; i++)
                sMatrix[i, i] = s[i];

            var uReduced = u.Get(0, numRows - 1, 0, s.Length - 1);
            var vReduced = v.Get(0, s.Length - 1, 0, numCols - 1);

            return uReduced.Multiply(sMatrix).Multiply(vReduced.Transpose());
        }

        private List<ProductRecommendationDTO> RecommendProducts(double[,] reconstructedMatrix, int customerId, List<ProductDetailDTO> allProducts)
        {
            var userIds = Enumerable.Range(0, reconstructedMatrix.GetLength(0)).ToArray();
            var productIds = Enumerable.Range(0, reconstructedMatrix.GetLength(1)).ToArray();

            int userIndex = Array.IndexOf(userIds, customerId);
            if (userIndex < 0)
            {
                throw new Exception($"Customer ID {customerId} not found in the matrix.");
            }

            var recommendations = new List<ProductRecommendationDTO>();

            for (int i = 0; i < productIds.Length; i++)
            {
                if (reconstructedMatrix[userIndex, i] > 0)
                {
                    var product = allProducts.FirstOrDefault(p => p.ProductID == productIds[i]);
                    if (product != null)
                    {
                        recommendations.Add(new ProductRecommendationDTO
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            CategoryName = product.CategoryName,
                            Price = product.price
                        });
                    }
                }
            }

            return recommendations.OrderByDescending(r => r.Price).ToList();
        }
    }
}