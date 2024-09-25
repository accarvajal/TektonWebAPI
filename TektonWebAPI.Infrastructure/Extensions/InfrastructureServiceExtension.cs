using MediatR;
using TektonWebAPI.Application.Features.Auth.Commands;
using TektonWebAPI.Common.Abstractions;
using TektonWebAPI.Infrastructure.Behaviors;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));

        services.AddHttpClient<IDiscountService, DiscountService>();

        services.AddApiDocumentation();
        services.AddAuthorization();
        services.AddAuthenticationService(configuration);
        services.AddCacheService(configuration);
        services.AddMappings();
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));
        services.AddPersistence(configuration);

        return services;
    }
}
