using BlazorAppUser.Pages.account;
using SharedLib.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class AccountService
    {
        public AccountService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<ResponseDTO<string>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PostAsJsonAsync<UserLoginDTO>("api/account/login", userLoginDTO);
                var responseDto = await response.Content.ReadFromJsonAsync<ResponseDTO<string>>();
                return responseDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }

        }

        public async Task<ResponseDTO<object>> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PostAsJsonAsync("api/account/register", userRegisterDTO);
                var responseDto = await response.Content.ReadFromJsonAsync<ResponseDTO<object>>();
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
