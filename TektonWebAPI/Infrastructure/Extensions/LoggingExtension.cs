using Serilog;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class LoggingExtension
{
    public static void ConfigureLogs(this ConfigureHostBuilder hostBuilder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
                    .CreateLogger();

        hostBuilder.UseSerilog();
    }
}
