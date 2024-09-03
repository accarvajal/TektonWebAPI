using TektonWebAPI.Application.Abstractions;
using TektonWebAPI.Application.Features.Products.Queries;
using TektonWebAPI.Core.Abstractions;

namespace TektonWebAPI.Application.Features.Products.Handlers;

public class GetProductByIdQueryHandler(
    IProductService productService,
    IProductStatusCache productStatusCache,
    IDiscountService discountService,
    IFinalPriceCalculator finalPriceCalculator,
    IMapper mapper
) : IRequestHandler<GetProductByIdQuery, Result<ProductResponseDto>>
{
    private readonly IProductService _productService = productService;
    private readonly IProductStatusCache _productStatusCache = productStatusCache;
    private readonly IDiscountService _discountService = discountService;
    private readonly IFinalPriceCalculator _finalPriceCalculator = finalPriceCalculator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ProductResponseDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _productService.GetByIdAsync(request.ProductId);

        if (result.IsFailure)
        {
            return Result<ProductResponseDto>.Failure(result.Error, result.ErrorCode);
        }

        var productDto = _mapper.Map<ProductResponseDto>(result.Value);

        // Get the discount from the external service
        var discount = await _discountService.GetDiscountAsync(request.ProductId);
        productDto.Discount = discount;

        // Calculate the final price by using the final price calculator service
        productDto.FinalPrice = _finalPriceCalculator.CalculateFinalPrice(productDto);

        return Result<ProductResponseDto>.Success(productDto);
    }
}