using SharedLib.AdminDTO;
using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class CategoriesService
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public CategoriesService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }


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
            try{
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
        public async Task<ResponseDTO<CategoryDTO>> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PostAsJsonAsync("/api/categories", categoryDTO);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<CategoryDTO>>();
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<object>> EditCategory(int id , string name)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PutAsJsonAsync($"/api/categories/{id}", name);
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
