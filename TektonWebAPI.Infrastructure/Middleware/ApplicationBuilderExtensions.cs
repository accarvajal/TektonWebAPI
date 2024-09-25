using Microsoft.AspNetCore.Builder;

namespace TektonWebAPI.Infrastructure.Middleware;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }

    public static IApplicationBuilder UseRequestExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }

    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestTimingMiddleware>();

        return app;
    }
}