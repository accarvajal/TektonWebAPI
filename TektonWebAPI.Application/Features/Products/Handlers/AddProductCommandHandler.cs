using TektonWebAPI.Application.Features.Products.Commands;

namespace TektonWebAPI.Application.Features.Products.Handlers;

public class AddProductCommandHandler(IProductService productService, IMapper mapper) : IRequestHandler<AddProductCommand, Result<int>>
{
    private readonly IProductService _productService = productService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.ProductDto);
        var result = await _productService.AddAsync(product);

        if (result.IsFailure)
        {
            return Result<int>.Failure(result.Error, result.ErrorCode);
        }

        return Result<int>.Success(product.ProductId);
    }
}