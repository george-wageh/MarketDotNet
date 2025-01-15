using Microsoft.AspNetCore.Components;
using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class OrdersService
    {
        public OrdersService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }
        public IHttpClientFactory HttpClientFactory { get; }



        public async Task<ResponseDTO<object>> PlaceOrder() {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.PostAsJsonAsync<object>($"/api/orders", null);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<object>>();
                return responseDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }


        public async Task<ResponseDTO<OrderDTO>> GetOrderAsync(int? orderId) {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<OrderDTO>>($"/api/orders/{orderId}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }


        public async Task<ResponseDTO<IEnumerable<OrderCardDTO>>> GetAllAsync() {
            try {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<OrderCardDTO>>>($"/api/orders");
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
