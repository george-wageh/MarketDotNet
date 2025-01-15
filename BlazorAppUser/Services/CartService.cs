using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class CartService
    {
        public CartService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }


        public async Task<ResponseDTO<object>> AddToCartAsync(int productId) {

            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.PostAsJsonAsync<object>($"api/cart/{productId}", null);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<object>>();
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        public async Task<ResponseDTO<IEnumerable<ProductQuantityCardDTO>>> GetAllProductsAsync()
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var responseDTO = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<ProductQuantityCardDTO>>>("api/cart/products");
                return responseDTO;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        public async Task<ResponseDTO<object>> DeleteAsync(int productId)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var responseDTO = await HttpClient.DeleteFromJsonAsync<ResponseDTO<object>>($"api/cart/{productId}");
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        public async Task<ResponseDTO<object>> ChnageQuantity(int productId , int Quantity)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.PostAsJsonAsync<int>($"api/cart/quantity/{productId}", Quantity);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<object>>();

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
