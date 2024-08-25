namespace TektonWebAPI.Application.Features.Products.Commands;

public class UpdateProductCommand(int productId, ProductRequestDto productDto) : IRequest<Result>
{
    public int ProductId { get; set; } = productId;
    public ProductRequestDto ProductDto { get; set; } = productDto;
}