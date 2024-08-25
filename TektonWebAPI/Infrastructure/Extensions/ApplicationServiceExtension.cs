using TektonWebAPI.Application.Interfaces;
using TektonWebAPI.Application.Services;
using TektonWebAPI.Infrastructure.Repositories;
using TektonWebAPI.Infrastructure.Services;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductStatusCache, ProductStatusCache>();
        services.AddScoped<IFinalPriceCalculator, FinalPriceCalculator>();

        services.AddHttpClient<IDiscountService, DiscountService>();

        return services;
    }
}
