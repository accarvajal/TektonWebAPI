using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using TektonWebAPI.Infrastructure.Configuration;
using TektonWebAPI.Infrastructure.Services;
using ZiggyCreatures.Caching.Fusion;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class CachingServiceExtension
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure caching options enabling IOptions<CacheSettings>
        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

        // Get cache configuration
        var cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();

        // Register cache services based on the strategy
        if (cacheSettings!.Strategy == "FusionCache")
        {
            // Hybrid cache that can work as memory cache (L1) or as distributed cache (L2) using any implementation of IDistributedCache, to get the most of both worlds.
            var fusionCacheBuilder = services.AddFusionCache()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromMinutes(cacheSettings.ExpirationMinutes),
                    Priority = CacheItemPriority.High
                });

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
