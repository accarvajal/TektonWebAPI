using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Text.Json;

namespace TektonWebAPI.Infrastructure.Services;

public class DistributedCacheService(IDistributedCache cache, IOptions<CacheSettings> settings) : ICacheService
{
    private readonly IDistributedCache _cache = cache;
    private readonly CacheSettings _settings = settings.Value;
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new ConcurrentDictionary<string, SemaphoreSlim>();

    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<T>(cachedData)!;
        }

        var myLock = _locks.GetOrAdd(cacheKey, _ => new SemaphoreSlim(1, 1));

        bool hasLock = await myLock.WaitAsync(500);
        if (!hasLock)
        {
            // Handle the case where the lock was not acquired within the timeout
            return default!;
        }


        try
        {
            // Double-check to see if another thread has already populated the cache
            cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<T>(cachedData)!;
            }

            var data = await factory();
            var serializedData = JsonSerializer.Serialize(data);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(_settings.ExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, serializedData, options);

            return data;
        }
        finally
        {
            myLock.Release();
            _locks.TryRemove(cacheKey, out _);
        }
    }
}