using TektonWebAPI.Infrastructure.Abstractions;
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
            services.AddFusionCache()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromMinutes(cacheSettings.ExpirationMinutes)
                });

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
