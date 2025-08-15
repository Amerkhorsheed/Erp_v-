// File: Services/IRecommendationService.cs
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Erp_V1.DAL.DTO;

namespace Erp_V1.Services
{
    public interface IRecommendationService
    {
        // Asynchronously gets recommendations for a customer.
        Task<List<ProductDetailDTO>> GetRecommendationsAsync(string customerName, CancellationToken ct = default);

        // Returns the cached “AvailableProducts” from the engine.
        List<ProductDetailDTO> GetAvailableProducts();
    }
}
