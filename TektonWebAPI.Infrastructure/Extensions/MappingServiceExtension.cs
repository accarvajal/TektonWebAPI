using TektonWebAPI.Application.Mappers.Profiles;

namespace TektonWebAPI.Infrastructure.Extensions;

internal static class MappingServiceExtension
{
    internal static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductProfile));

        return services;
    }
}
