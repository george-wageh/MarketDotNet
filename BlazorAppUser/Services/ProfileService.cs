using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class ProfileService
    {
        public IHttpClientFactory HttpClientFactory { get; }
        public ILocalStorageService LocalStorageService { get; }
        public NavigationManager NavigationManager { get; }

        public ProfileService(IHttpClientFactory HttpClientFactory , ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            this.HttpClientFactory = HttpClientFactory;
            LocalStorageService = localStorageService;
            NavigationManager = navigationManager;
        }
        public async Task<ResponseDTO<ProfileDTO>> GetAsync() {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<ProfileDTO>>($"api/profile");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        public async Task Logout()
        {
            try
            {
                await LocalStorageService.RemoveItemAsync("token");
                NavigationManager.NavigateTo("login");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
        }
    }
}
