using TektonWebAPI.Application.Services;

namespace TektonWebAPI.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductStatusCache, ProductStatusCache>();
        services.AddScoped<IFinalPriceCalculator, FinalPriceCalculator>();
        
        services.AddValidationServices();

        return services;
    }
}
