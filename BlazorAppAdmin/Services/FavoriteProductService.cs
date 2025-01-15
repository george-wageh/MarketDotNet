using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class FavoriteProductService
    {
        public HttpClient HttpClient { get; }
        public IHttpClientFactory HttpClientFactory { get; }

        public FavoriteProductService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO<IEnumerable<ProductCardDTO>>> GetAllFavListAsync(string userName)
        {
            try { 
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<ProductCardDTO>>>($"api/AdminFavoriteProducts/{userName}");
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
