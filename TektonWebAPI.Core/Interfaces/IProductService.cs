namespace TektonWebAPI.Core.Interfaces;

public interface IProductService
{
    Task<Result<Product>> GetByIdAsync(int productId);
    Task<Result> AddAsync(Product product);
    Task<Result> UpdateAsync(Product product);
}
