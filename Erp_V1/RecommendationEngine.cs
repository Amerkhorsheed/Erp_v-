using System;
using System.Collections.Generic;
using System.Linq;
using Erp_V1.DAL.DTO;
using Erp_V1.BLL;

namespace Erp_V1
{
    public class RecommendationEngine
    {
        private SalesBLL salesBLL = new SalesBLL();
        private ProductBLL productBLL = new ProductBLL();

        // Method to get recommendations for a customer
        public List<ProductRecommendationDTO> GetRecommendationsForCustomer(int customerId)
        {
            var allSales = salesBLL.Select().Sales;
            var allProducts = productBLL.Select().Products;

            // Fetch the customer's purchase history
            var customerSales = allSales.Where(s => s.CustomerID == customerId).ToList();

            if (!customerSales.Any())  // If the customer has no sales history
            {
                Console.WriteLine($"Customer {customerId} has no sales history.");
                return new List<ProductRecommendationDTO>();  // Return empty list
            }

            // Get the products and categories they frequently buy
            var frequentProducts = GetFrequentProducts(customerSales);
            var preferredCategories = GetPreferredCategories(customerSales);

            // Get a price range they are most comfortable with
            var preferredPriceRange = GetPreferredPriceRange(customerSales);

            // Generate product recommendations based on their behavior
            var recommendations = RecommendProducts(frequentProducts, preferredCategories, preferredPriceRange, allProducts);

            if (!recommendations.Any())
            {
                Console.WriteLine($"No recommendations available for Customer {customerId} based on strict criteria.");
                // Fallback: Try a wider price range or related categories
                recommendations = FallbackRecommendations(preferredCategories, preferredPriceRange, allProducts);
            }

            return recommendations;
        }

        // Method to get frequent products bought by the customer
        private List<int> GetFrequentProducts(List<SalesDetailDTO> sales)
        {
            return sales.GroupBy(s => s.ProductID)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key)
                        .ToList();
        }

        // Method to get preferred categories
        private List<int> GetPreferredCategories(List<SalesDetailDTO> sales)
        {
            return sales.GroupBy(s => s.CategoryID)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key)
                        .ToList();
        }

        // Method to get preferred price range (average price of products they bought)
        private Tuple<decimal, decimal> GetPreferredPriceRange(List<SalesDetailDTO> sales)
        {
            var avgPrice = (decimal)sales.Average(s => (double)s.Price);
            var lowerBound = avgPrice * 0.8m;  // Lower bound of price
            var upperBound = avgPrice * 1.2m;  // Upper bound of price

            return new Tuple<decimal, decimal>(lowerBound, upperBound);
        }

        // Recommend products based on preferences
        private List<ProductRecommendationDTO> RecommendProducts(
            List<int> frequentProductIds,
            List<int> preferredCategoryIds,
            Tuple<decimal, decimal> preferredPriceRange,
            List<ProductDetailDTO> allProducts)
        {
            var recommendedProducts = allProducts.Where(p =>
                preferredCategoryIds.Contains(p.CategoryID) &&
                p.price >= preferredPriceRange.Item1 &&
                p.price <= preferredPriceRange.Item2 &&
                !frequentProductIds.Contains(p.ProductID)) // Exclude products already bought frequently
                .Select(p => new ProductRecommendationDTO
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    CategoryName = p.CategoryName,
                    Price = p.price
                })
                .ToList();

            return recommendedProducts;
        }

        // Fallback logic to get recommendations if strict criteria don't return results
        private List<ProductRecommendationDTO> FallbackRecommendations(
            List<int> preferredCategoryIds,
            Tuple<decimal, decimal> preferredPriceRange,
            List<ProductDetailDTO> allProducts)
        {
            // Widen price range by 50%
            var widenedLowerBound = preferredPriceRange.Item1 * 0.5m;
            var widenedUpperBound = preferredPriceRange.Item2 * 1.5m;

            var fallbackProducts = allProducts.Where(p =>
                preferredCategoryIds.Contains(p.CategoryID) &&
                p.price >= widenedLowerBound &&
                p.price <= widenedUpperBound)
                .Select(p => new ProductRecommendationDTO
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    CategoryName = p.CategoryName,
                    Price = p.price
                })
                .ToList();

            return fallbackProducts;
        }
    }
}
