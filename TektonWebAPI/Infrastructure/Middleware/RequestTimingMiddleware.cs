using System.Diagnostics;

namespace TektonWebAPI.Infrastructure.Middleware;

public class RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestTimingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var startTime = DateTime.Now;

        await _next(context);

        stopwatch.Stop();
        var endTime = DateTime.Now;

        var logMessage = $"Request {context.Request.Method} {context.Request.Path} started at {startTime} and ended at {endTime} taking {stopwatch.ElapsedMilliseconds}ms";
        _logger.LogInformation(logMessage);

        await File.AppendAllTextAsync("logs.txt", logMessage + Environment.NewLine);
    }
}
