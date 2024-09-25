using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace TektonWebAPI.Application.Services;

public class ProductStatusCache(IDistributedCache cache) : IProductStatusCache
{
    private readonly IDistributedCache _cache = cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public Dictionary<int, string> GetProductStatuses()
    {
        var cachedData = _cache.GetString("ProductStatuses");
        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<Dictionary<int, string>>(cachedData)!;
        }

        var statuses = new Dictionary<int, string>
            {
                { 1, "Active" },
                { 0, "Inactive" }
            };

        var serializedData = JsonSerializer.Serialize(statuses);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };

        _cache.SetString("ProductStatuses", serializedData, options);

        return statuses!;
    }
}