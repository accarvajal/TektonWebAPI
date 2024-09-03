namespace TektonWebAPI.Core.Abstractions;

public interface IDiscountService
{
    Task<decimal> GetDiscountAsync(int productId);
}