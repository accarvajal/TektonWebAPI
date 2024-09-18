namespace TektonWebAPI.Infrastructure.Configuration;

public record DistributedCacheSettings
{
    public bool UseRedis { get; set; } = false;
    public string RedisConfiguration { get; set; } = string.Empty;
}