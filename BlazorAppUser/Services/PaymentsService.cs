using SharedLib.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class PaymentsService
    {
        public PaymentsService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }


        public async Task<ResponseDTO<IEnumerable<PaymentCardDTO>>> GetAllAsync()
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<PaymentCardDTO>>>($"api/payments");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<PaymentCardDTO>> GetDefaultAsync()
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<PaymentCardDTO>>($"api/payments/GetDefaultAddress");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<PaymentCardDTO>> AddAsync(PaymentDTO paymentDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PostAsJsonAsync<PaymentDTO>($"api/payments", paymentDTO);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<PaymentCardDTO>>();
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }


        public async Task<ResponseDTO<object>> DeleteAsync(int paymentId)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var responseDTO = await HttpClient.DeleteFromJsonAsync<ResponseDTO<object>>($"api/payments/{paymentId}");
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        public async Task<ResponseDTO<object>> setDefaultAsync(int paymentId)
        {
            try
            {

                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PostAsJsonAsync<int>($"api/payments/setDefault", paymentId);
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
