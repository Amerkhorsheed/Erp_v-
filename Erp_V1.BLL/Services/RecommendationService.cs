// File: Services/RecommendationService.cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Erp_V1.Services
{
    public sealed class RecommendationService : IRecommendationService
    {
        private const int CacheDurationMinutes = 15;
        private readonly IRecommendationEngine _engine;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RecommendationService> _logger;
        private static readonly MemoryCacheEntryOptions CacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheDurationMinutes)
        };

        public RecommendationService(
            IRecommendationEngine engine,
            IMemoryCache cache,
            ILogger<RecommendationService> logger)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<ProductDetailDTO>> GetRecommendationsAsync(
            string customerName, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name is required", nameof(customerName));

            string cacheKey = $"Recs_{customerName.Trim().ToLowerInvariant()}";

            if (_cache.TryGetValue(cacheKey, out List<ProductDetailDTO> cached))
            {
                _logger.LogDebug("Cache hit for {Customer}", customerName);
                return cached;
            }

            using (_logger.BeginScope(new Dictionary<string, object> { { "Customer", customerName } }))
            {
                _logger.LogInformation("Generating recommendations");
                var sw = System.Diagnostics.Stopwatch.StartNew();

                // Offload the engine call to a background thread
                var result = await Task.Run(() =>
                    _engine.GetRecommendationsForCustomer(customerName), ct);

                _cache.Set(cacheKey, result, CacheOptions);
                _logger.LogInformation(
                    "Generated {Count} recommendations in {Elapsed}ms",
                    result.Count,
                    sw.ElapsedMilliseconds);

                return result;
            }
        }

        public List<ProductDetailDTO> GetAvailableProducts()
        {
            return _engine.AvailableProducts;
        }
    }
}
