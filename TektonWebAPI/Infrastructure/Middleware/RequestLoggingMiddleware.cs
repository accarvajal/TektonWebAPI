using System.Diagnostics;

namespace TektonWebAPI.Infrastructure.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        var logMessage = $"Request {context.Request.Method} {context.Request.Path} executed in {stopwatch.ElapsedMilliseconds}ms";
        _logger.LogInformation(logMessage);
        await File.AppendAllTextAsync("logs.txt", logMessage + Environment.NewLine);
    }
}
