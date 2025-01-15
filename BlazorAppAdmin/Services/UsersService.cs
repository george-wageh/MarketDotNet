using SharedLib.AdminDTO;
using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class UsersService
    {
        public UsersService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<ResponseListDTO<IEnumerable<UserDTO>>> GetUsers(UserQueryDTO userQueryDTO)
        {
            HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
            var response = await HttpClient.PostAsJsonAsync<UserQueryDTO>("api/users/GetUsers", userQueryDTO);
            var responseDTO = await response.Content.ReadFromJsonAsync<ResponseListDTO<IEnumerable<UserDTO>>>();
            return responseDTO;
        }
    }
}
