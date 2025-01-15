using SharedLib.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class PaymentsService
    {
        public PaymentsService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<ResponseDTO<IEnumerable<PaymentCardDTO>>> GetAllAsync(string userName)
        {
            try
            {
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<PaymentCardDTO>>>($"api/AdminPayments/{userName}");
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
