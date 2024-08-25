using System.Text.Json;

namespace TektonWebAPI.Infrastructure.Services;

public class DiscountService(HttpClient httpClient) : IDiscountService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<decimal> GetDiscountAsync(int productId)
    {
        // Simulate the discount by using the result from an external service
        var response = await _httpClient.GetStringAsync($"https://mockapi.io/discounts/{productId}");
        var discount = JsonSerializer.Deserialize<decimal>(response);
        return discount;
    }
}