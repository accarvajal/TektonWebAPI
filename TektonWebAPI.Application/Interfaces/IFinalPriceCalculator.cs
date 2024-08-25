namespace TektonWebAPI.Application.Interfaces;

public interface IFinalPriceCalculator
{
    decimal CalculateFinalPrice(ProductResponseDto product);
}