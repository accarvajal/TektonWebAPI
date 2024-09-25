using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace TektonWebAPI.Infrastructure.Extensions;

internal static class CachingServiceExtension
{
    internal static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure caching options enabling IOptions<CacheSettings>
        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

        // Get cache configuration
        var cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();

        // Register cache services based on the strategy
        if (cacheSettings!.Strategy == "FusionCache")
        {
            // This service is added just to supply the DI for ProductStatusCache
            services.AddDistributedMemoryCache();

            // Hybrid cache that can work as memory cache (L1) or as distributed cache (L2) using any implementation of IDistributedCache, to get the most of both worlds.
            var fusionCacheBuilder = services.AddFusionCache()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromMinutes(cacheSettings.ExpirationMinutes),
                    Priority = CacheItemPriority.High
                })
                .WithSerializer(new FusionCacheSystemTextJsonSerializer());

            // Configure distributed cache if "DistributedCache" entry is present
            if (cacheSettings.DistributedCache?.UseRedis == true)
            {
                fusionCacheBuilder.WithDistributedCache(new RedisCache(
                    new RedisCacheOptions
                    {
                        Configuration = cacheSettings.DistributedCache.RedisConfiguration
                    }
                ));
            }

            services.AddTransient<ICacheService, FusionCacheService>();
        }
        else
        {
            services.AddDistributedMemoryCache();

            if (cacheSettings.DistributedCache?.UseRedis == true)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = cacheSettings.DistributedCache.RedisConfiguration;
                });
            }

            services.AddTransient<ICacheService, DistributedCacheService>();
        }

        return services;
    }
}
