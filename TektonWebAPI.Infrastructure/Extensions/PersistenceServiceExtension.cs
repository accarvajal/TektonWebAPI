using Microsoft.EntityFrameworkCore;

namespace TektonWebAPI.Infrastructure.Extensions;

internal static class PersistenceServiceExtension
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
