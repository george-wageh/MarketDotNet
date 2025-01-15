using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class AddressService
    {
        public AddressService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<ResponseDTO<IEnumerable<AddressDTO>>> GetAllAsync(string userName)
        {
            try
            {
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<AddressDTO>>>($"api/AdminAddresses/{userName}");
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
