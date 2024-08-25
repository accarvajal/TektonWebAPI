using Microsoft.Extensions.Caching.Memory;

namespace TektonWebAPI.Application.Services;

public class ProductStatusCache(IMemoryCache cache) : IProductStatusCache
{
    private readonly IMemoryCache _cache = cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public Dictionary<int, string> GetProductStatuses()
    {
        if (!_cache.TryGetValue("ProductStatuses", out Dictionary<int, string>? statuses))
        {
            statuses = new Dictionary<int, string>
                {
                    { 1, "Active" },
                    { 0, "Inactive" }
                };
            
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            };

            _cache.Set("ProductStatuses", statuses, cacheEntryOptions);
        }

        return statuses!;
    }
}