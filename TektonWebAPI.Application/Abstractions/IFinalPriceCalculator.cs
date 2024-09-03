namespace TektonWebAPI.Application.Abstractions;

public interface IFinalPriceCalculator
{
    decimal CalculateFinalPrice(ProductResponseDto product);
}