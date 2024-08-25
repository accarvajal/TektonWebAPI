namespace TektonWebAPI.Application.Features.Products.Queries;

public class GetProductByIdQuery : IRequest<Result<ProductResponseDto>>
{
    public int ProductId { get; set; }

    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }
}
