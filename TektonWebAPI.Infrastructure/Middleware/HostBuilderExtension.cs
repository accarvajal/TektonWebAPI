using Microsoft.AspNetCore.Builder;
using Serilog;

namespace TektonWebAPI.Infrastructure.Middleware;

public static class HostBuilderExtension
{
    public static void ConfigureLogs(this ConfigureHostBuilder hostBuilder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
                    .CreateLogger();

        hostBuilder.UseSerilog();
    }
}
