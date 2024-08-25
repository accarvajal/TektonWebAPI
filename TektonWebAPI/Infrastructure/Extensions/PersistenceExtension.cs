using Microsoft.EntityFrameworkCore;
using TektonWebAPI.Infrastructure.Data;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
