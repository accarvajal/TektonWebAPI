namespace TektonWebAPI.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Result<Product>> GetByIdAsync(int productId)
    {
        var result = await _productRepository.GetByIdAsync(productId);

        if (result.IsFailure)
        {
            return Result<Product>.Failure(result.Error, result.ErrorCode);
        }

        return Result<Product>.Success(result.Value!);
    }

    public async Task<Result> AddAsync(Product product)
    {
        var result = await _productRepository.AddAsync(product);

        if (result.IsFailure)
        {
            return Result<Product>.Failure(result.Error, result.ErrorCode);
        }

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Product product)
    {
        var result = await _productRepository.UpdateAsync(product);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error, result.ErrorCode);
        }

        return Result.Success();
    }
}