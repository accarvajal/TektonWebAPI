namespace TektonWebAPI.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/auth/login", async (LoginCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: () => Results.Ok(new { Token = result.Value }),
                onFailure: error => Results.Json(new { Message = error }, statusCode: StatusCodes.Status401Unauthorized));
        });
    }
}