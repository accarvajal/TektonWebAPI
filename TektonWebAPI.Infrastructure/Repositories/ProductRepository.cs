using Microsoft.EntityFrameworkCore;

namespace TektonWebAPI.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext context) : IProductRepository
{
    private readonly ProductDbContext _context = context;

    public async Task<Result<Product>> GetByIdAsync(int productId)
    {
        try
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return Result<Product>.Failure(ProductErrors.NotFound(productId));
            }

            return Result<Product>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<Product>.Failure(
                CommonErrors.GeneralError($"An error occurred while retrieving the product Id {productId} => {ex.Message}"));
        }
    }

    public async Task<Result> AddAsync(Product product)
    {
        try
        {
            var productQuery = await _context.Products.FindAsync(product.ProductId);

            if (productQuery != null)
            {
                return Result.Failure(ProductErrors.AlreadyExists(product.ProductId));
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result<Product>.Failure(
                CommonErrors.GeneralError($"An error occurred while adding the product {product.Name} => {ex.Message}"));
        }
    }

    public async Task<Result> UpdateAsync(Product product)
    {
        try
        {
            var productQuery = await _context.Products.FindAsync(product.ProductId);

            if (productQuery == null)
            {
                return Result<Product>.Failure(ProductErrors.NotFound(product.ProductId));
            }

            // Detach the existing entity to avoid tracking issues
            _context.Entry(productQuery).State = EntityState.Detached;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result<Product>.Failure(
                CommonErrors.GeneralError($"An error occurred while updating the product Id {product.ProductId} => {ex.Message}"));
        }
    }
}