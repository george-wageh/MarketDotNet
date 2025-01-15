using BlazorAppAdmin.Pages.Users;
using BlazorAppAdmin.Shared;
using Microsoft.AspNetCore.Components;
using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class OrdersService
    {
        public OrdersService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }
        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<ResponseDTO<OrderDTO>> GetOrderAsync(string userName, int? orderId) {
            try
            {
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<OrderDTO>>($"/api/adminOrders/{userName}/{orderId}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }


        public async Task<ResponseDTO<IEnumerable<OrderCardDTO>>> GetAllAsync(string userName) {
            try {
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<OrderCardDTO>>>($"/api/adminOrders/{userName}");
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
