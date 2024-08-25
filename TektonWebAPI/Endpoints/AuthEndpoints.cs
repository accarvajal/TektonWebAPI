namespace TektonWebAPI.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/auth/login", async (LoginCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                //return Results.Unauthorized();
                return Results.Json(new { Message = result.Error }, statusCode: StatusCodes.Status401Unauthorized);
            }

            return Results.Ok(new { Token = result.Value });
        });
    }
}