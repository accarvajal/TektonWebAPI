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
                return Result<Product>.Failure("Product not found.");
            }

            return Result<Product>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<Product>.Failure($"An error occurred while retrieving the product: {ex.Message}");
        }
    }

    public async Task<Result> AddAsync(Product product)
    {
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"An error occurred while adding the product: {ex.Message}");
        }
    }

    public async Task<Result> UpdateAsync(Product product)
    {
        try
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"An error occurred while updating the product: {ex.Message}");
        }
    }
}