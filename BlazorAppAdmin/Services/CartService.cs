using BlazorAppAdmin.Shared;
using SharedLib.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class CartService
    {
        public CartService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<ResponseDTO<IEnumerable<ProductQuantityCardDTO>>> GetAllProductsAsync(string userName)
        {
            try
            {
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
                var responseDTO = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<ProductQuantityCardDTO>>>($"api/AdminCart/{userName}");
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

    }
}
