using SharedLib.AdminDTO;
using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class CategoriesService
    {
        public CategoriesService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }


        public async Task<ResponseDTO<IEnumerable<CategoryDTO>>> GetAllCategories() {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var responseDto = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<CategoryDTO>>>("/api/categories");
                return responseDto;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        public async Task<ResponseDTO<CategoryDTO>> GetCategory(int id)
        {

            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var responseDto = await HttpClient.GetFromJsonAsync<ResponseDTO<CategoryDTO>>($"/api/categories/{id}");
                return responseDto;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
    }
}
