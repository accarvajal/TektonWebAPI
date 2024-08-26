namespace TektonWebAPI.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Result<Product>> GetByIdAsync(int productId)
    {
        try
        {
            var result = await _productRepository.GetByIdAsync(productId);

            if (result.IsFailure)
            {
                return Result<Product>.Failure($"Product with ID {productId} was not found.");
            }

            return Result<Product>.Success(result.Value!);
        }
        catch
        {
            return Result<Product>.Failure("An error occurred while retrieving the product.");
        }
    }

    public async Task<Result> AddAsync(Product product)
    {
        try
        {
            await _productRepository.AddAsync(product);
            return Result.Success();
        }
        catch
        {
            return Result.Failure("An error occurred while adding the product.");
        }
    }

    public async Task<Result> UpdateAsync(Product product)
    {
        try
        {
            var result = await _productRepository.UpdateAsync(product);

            if (result.IsFailure)
            {
                return Result<Product>.Failure($"Product with ID {product.ProductId} was not found.");
            }

            return Result.Success();
        }
        catch
        {
            return Result.Failure("An error occurred while updating the product.");
        }
    }
}