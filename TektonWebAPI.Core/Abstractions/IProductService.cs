using TektonWebAPI.Core.Common;

namespace TektonWebAPI.Core.Abstractions;

public interface IProductService
{
    Task<Result<Product>> GetByIdAsync(int productId);
    Task<Result> AddAsync(Product product);
    Task<Result> UpdateAsync(Product product);
}
