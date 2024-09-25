using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using TektonWebAPI.Common.Abstractions;
using TektonWebAPI.Common.Constants;

namespace TektonWebAPI.Infrastructure.Behaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger,
    ICurrentUserService currentUserService,
    IConfiguration configuration
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger = logger;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly string _minimumLogLevel = configuration.GetSection(ConfigurationKeys.SerilogMinimumLevel).Value ?? "Information";

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        var user = _currentUserService.GetCurrentUserId();

        _logger.LogInformation("Processing request {RequestName} for user {User}", requestName, user);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            _logger.LogInformation("Completed request {RequestName} for user {User}", requestName, user);
        }
        else
        {
            var isDetailedLoggingEnabled = _minimumLogLevel == "Debug" || _minimumLogLevel == "Information";

            if (result.Error.Code == CommonErrors.CommonGeneralError)
            {
                if (isDetailedLoggingEnabled)
                {
                    using (LogContext.PushProperty("Error", result.Error, true))
                    {
                        _logger.LogError("Completed request {RequestName} for user '{User}' with error: {ErrorMessage}", requestName, user, result.Error.Description);
                    }
                }
                else
                {
                    _logger.LogError("Completed request {RequestName} for user '{User}' with error. Contact Administrator to see log details.", requestName, user);
                }
            }
            else
            {
                _logger.LogWarning("Completed request {RequestName} for user '{User}' with specific message: {WarningMessage}", requestName, user, result.Error.Description);
            }
        }

        return result;
    }
}