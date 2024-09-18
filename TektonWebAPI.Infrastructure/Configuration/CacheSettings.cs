namespace TektonWebAPI.Infrastructure.Configuration;

public record CacheSettings
{
    public required string Strategy { get; set; }
    public required int ExpirationMinutes { get; set; } = 60;
    public DistributedCacheSettings? DistributedCache { get; set; } = default;
}