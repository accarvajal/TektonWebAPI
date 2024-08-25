namespace TektonWebAPI.Application.Features.Products.Commands;

public class AddProductCommand(ProductRequestDto productDto) : IRequest<Result<int>>
{
    public ProductRequestDto ProductDto { get; set; } = productDto;
}