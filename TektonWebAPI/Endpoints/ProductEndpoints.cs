using TektonWebAPI.Application.Features.Products.Commands;
using TektonWebAPI.Application.Features.Products.Queries;
using TektonWebAPI.Application.Validations;
using TektonWebAPI.Core.Utilities;

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
                return result.ErrorCode switch
                {
                    ErrorCode.ProductNotFound => Results.NotFound(new { Message = result.Error }),
                    _ => Results.BadRequest(new { Message = result.Error })
                };
            }

            return Results.Ok(result.Value);
        }).RequireAuthorization();

        endpoints.MapPost("/api/product", async (ProductRequestDto productDto, IMediator mediator) =>
        {
            var validator = new ProductValidator();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var command = new AddProductCommand(productDto);
            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                return result.ErrorCode switch
                {
                    ErrorCode.ProductAlreadyExists => Results.Conflict(new { Message = result.Error }),
                    _ => Results.BadRequest(new { Message = result.Error })
                };
            }

            return Results.Created($"/api/product/{result.Value}", new { Id = result.Value });
        }).RequireAuthorization();

        endpoints.MapPut("/api/product/{id}", async (int id, ProductRequestDto productDto, IMediator mediator) =>
        {
            var validator = new ProductValidator();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var command = new UpdateProductCommand(id, productDto);
            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                return result.ErrorCode switch
                {
                    ErrorCode.ProductNotFound => Results.NotFound(new { Message = result.Error }),
                    _ => Results.BadRequest(new { Message = result.Error })
                };
            }
            return Results.NoContent();
        }).RequireAuthorization();
    }
}