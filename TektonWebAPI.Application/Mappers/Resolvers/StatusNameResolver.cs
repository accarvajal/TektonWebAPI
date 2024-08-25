namespace TektonWebAPI.Application.Mappers.Resolvers;

public class StatusNameResolver(IProductStatusCache productStatusCache) : IValueResolver<Product, ProductResponseDto, string>
{
    private readonly IProductStatusCache _productStatusCache = productStatusCache;

    public string Resolve(Product source, ProductResponseDto destination, string destMember, ResolutionContext context)
    {
        var statuses = _productStatusCache.GetProductStatuses();
        return statuses.TryGetValue(source.Status, out var statusName) ? statusName : "Unknown";
    }
}