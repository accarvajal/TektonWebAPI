using Microsoft.Extensions.Options;
using TektonWebAPI.Infrastructure.Abstractions;
using TektonWebAPI.Infrastructure.Configuration;
using ZiggyCreatures.Caching.Fusion;

namespace TektonWebAPI.Infrastructure.Services;

public class FusionCacheService(IFusionCache cache, IOptions<CacheSettings> settings) : ICacheService
{
    private readonly IFusionCache _cache = cache;
    private readonly CacheSettings _settings = settings.Value;

    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        return await _cache.GetOrSetAsync(
            cacheKey,
            async _ => await factory(),
            options => options.SetDuration(expiration ?? TimeSpan.FromMinutes(_settings.ExpirationMinutes))
        );
    }
}
