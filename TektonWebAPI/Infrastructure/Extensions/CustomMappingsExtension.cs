using TektonWebAPI.Application.Mappers.Profiles;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class CustomMappingsExtension
{
    public static IServiceCollection AddCustomMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductProfile));

        return services;
    }
}
