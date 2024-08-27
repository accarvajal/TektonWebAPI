using Microsoft.EntityFrameworkCore;
using TektonWebAPI.Core.Entities;

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
                return Result<Product>.Failure($"Product with ID {productId} was not found.", ErrorCode.ProductNotFound);
            }

            return Result<Product>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<Product>.Failure($"An error occurred while retrieving the product: {ex.Message}", ErrorCode.GeneralError);
        }
    }

    public async Task<Result> AddAsync(Product product)
    {
        try
        {
            var productQuery = await _context.Products.FindAsync(product.ProductId);

            if (productQuery != null)
            {
                return Result.Failure($"Product with ID {product.ProductId} already exists.", ErrorCode.ProductAlreadyExists);
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"An error occurred while adding the product: {ex.Message}", ErrorCode.GeneralError);
        }
    }

    public async Task<Result> UpdateAsync(Product product)
    {
        try
        {
            var productQuery = await _context.Products.FindAsync(product.ProductId);

            if (productQuery == null)
            {
                return Result<Product>.Failure($"Product with ID {product.ProductId} was not found.", ErrorCode.ProductNotFound);
            }

            // Detach the existing entity to avoid tracking issues
            _context.Entry(productQuery).State = EntityState.Detached;


            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"An error occurred while updating the product: {ex.Message}", ErrorCode.GeneralError);
        }
    }
}