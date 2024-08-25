namespace TektonWebAPI.Core.Interfaces;

public interface IDiscountService
{
    Task<decimal> GetDiscountAsync(int productId);
}