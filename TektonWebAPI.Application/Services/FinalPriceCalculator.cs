namespace TektonWebAPI.Application.Services;

public class FinalPriceCalculator : IFinalPriceCalculator
{
    public decimal CalculateFinalPrice(ProductResponseDto product)
    {
        return product.Price * (100 - product.Discount) / 100;
    }
}