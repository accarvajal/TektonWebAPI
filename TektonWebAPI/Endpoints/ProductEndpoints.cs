using TektonWebAPI.Application.Features.Products.Commands;
using TektonWebAPI.Application.Features.Products.Queries;

namespace TektonWebAPI.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/products/{id}", async (int id, IMediator mediator) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);

            return result.Match(
                onSuccess: () => Results.Ok(result.Value),
                onFailure: error => error.Code switch
                {
                    ProductErrors.ProductNotFound => Results.NotFound(new { Message = error.Description }),
                    _ => Results.BadRequest(new { Message = error.Description })
                });
        })
        .RequireAuthorization()
        .Produces<ProductResponseDto>(StatusCodes.Status200OK);

        endpoints.MapPost("/api/products", async (ProductRequestDto productDto, IMediator mediator) =>
        {
            var validator = new ProductValidator();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var command = new AddProductCommand(productDto);
            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: () => Results.Created($"/api/product/{result.Value}", new { Id = result.Value }),
                onFailure: error => error.Code switch
                {
                    ProductErrors.ProductAlreadyExists => Results.Conflict(new { Message = error.Description }),
                    _ => Results.BadRequest(new { Message = error.Description })
                });
        })
        .RequireAuthorization()
        .Produces<int>(StatusCodes.Status201Created);

        endpoints.MapPut("/api/products/{id}", async (int id, ProductRequestDto productDto, IMediator mediator) =>
        {
            var validator = new ProductValidator();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var command = new UpdateProductCommand(id, productDto);
            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: () => Results.NoContent(),
                onFailure: error => error.Code switch
                {
                    ProductErrors.ProductNotFound => Results.NotFound(new { Message = error.Description }),
                    _ => Results.BadRequest(new { Message = error.Description })
                });
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status204NoContent);
    }
}