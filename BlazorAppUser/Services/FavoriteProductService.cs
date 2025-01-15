using SharedLib.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class FavoriteProductService
    {
        public IHttpClientFactory HttpClientFactory { get; }


        public FavoriteProductService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }
        public async Task<ResponseDTO<object>> RemoveProductFromFav(int productId)
        {

            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.DeleteFromJsonAsync<ResponseDTO<object>>($"api/favoriteProducts/{productId}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<object>> AddProductToFav(int productId)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PostAsJsonAsync<int>("api/favoriteProducts", productId);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<object>>();
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<IEnumerable<ProductCardDTO>>> GetAllFavListAsync()
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<ProductCardDTO>>>("api/favoriteProducts");
                return response;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }

        }
    }
}
