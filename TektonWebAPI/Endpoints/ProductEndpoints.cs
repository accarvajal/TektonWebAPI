using TektonWebAPI.Application.Features.Products.Commands;
using TektonWebAPI.Application.Features.Products.Queries;

namespace TektonWebAPI.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/product/{id}", async (int id, IMediator mediator) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound(new { Message = result.Error });
            }

            return Results.Ok(result.Value);
        }).RequireAuthorization();

        endpoints.MapPost("/api/product", async (ProductRequestDto productDto, IMediator mediator) =>
        {
            var command = new AddProductCommand(productDto);
            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { Message = result.Error });
            }

            return Results.Created($"/api/product/{result.Value}", new { Id = result.Value });
        }).RequireAuthorization();

        endpoints.MapPut("/api/product/{id}", async (int id, ProductRequestDto productDto, IMediator mediator) =>
        {
            var command = new UpdateProductCommand(id, productDto);
            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { Message = result.Error });
            }
            return Results.NoContent();
        }).RequireAuthorization();
    }
}