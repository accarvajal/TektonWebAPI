namespace TektonWebAPI.Application.Mappers.Resolvers;

public class StatusNameResolver : IValueResolver<Product, ProductResponseDto, string>
{
    private readonly IProductStatusCache _productStatusCache;

    public StatusNameResolver(IProductStatusCache productStatusCache)
    {
        _productStatusCache = productStatusCache;
    }

    public string Resolve(Product source, ProductResponseDto destination, string destMember, ResolutionContext context)
    {
        var statuses = _productStatusCache.GetProductStatuses();
        return statuses.TryGetValue(source.Status, out var statusName) ? statusName : "Unknown";
    }
}