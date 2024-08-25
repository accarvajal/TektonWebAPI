using TektonWebAPI.Application.Features.Products.Commands;

namespace TektonWebAPI.Application.Features.Products.Handlers;

public class UpdateProductCommandHandler(IProductService productService, IMapper mapper) : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IProductService _productService = productService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.ProductId != request.ProductDto.ProductId)
        {
            return Result.Failure("Product ID mismatch.");
        }

        var product = _mapper.Map<Product>(request.ProductDto);
        var result = await _productService.UpdateAsync(product);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}