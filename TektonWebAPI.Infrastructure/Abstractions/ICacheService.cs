namespace TektonWebAPI.Infrastructure.Abstractions;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? expiration = null);
}