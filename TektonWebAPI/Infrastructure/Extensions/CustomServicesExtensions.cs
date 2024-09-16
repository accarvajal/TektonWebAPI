using TektonWebAPI.Application.Behaviors;
using TektonWebAPI.Common.Abstractions;
using TektonWebAPI.Infrastructure.Services;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class CustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
