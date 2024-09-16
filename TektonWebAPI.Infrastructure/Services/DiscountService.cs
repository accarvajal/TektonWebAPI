using Newtonsoft.Json.Linq;

namespace TektonWebAPI.Infrastructure.Services;

public class DiscountService(HttpClient httpClient) : IDiscountService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<decimal> GetDiscountAsync(int productId)
    {
        // Simulate the discount by using the result from an external service
        var response = await _httpClient.GetStringAsync($"https://66cc98efa4dd3c8a71b82d4d.mockapi.io/api/v1/discounts/{productId}");
        var discount = JObject.Parse(response)["discountValue"]?.Value<int>() ?? 0;
        return discount;
    }
}