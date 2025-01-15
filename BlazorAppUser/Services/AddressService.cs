using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class AddressService
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public AddressService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }

        

        public async Task<ResponseDTO<IEnumerable<AddressDTO>>> GetAllAsync()
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<AddressDTO>>>($"api/addresses");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<AddressDTO>> GetDefaultAsync()
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<AddressDTO>>($"api/addresses/GetDefaultAddress");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<AddressDTO>> AddAsync(AddressDTO addressDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.PostAsJsonAsync<AddressDTO>($"api/addresses" , addressDTO);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<AddressDTO>>();
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }


        public async Task<ResponseDTO<AddressDTO>> EditAsync(AddressDTO addressDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.PutAsJsonAsync<AddressDTO>($"api/addresses", addressDTO);
                var responseDTO = await response.Content.ReadFromJsonAsync<ResponseDTO<AddressDTO>>();
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<ResponseDTO<object>> DeleteAsync(AddressDTO addressDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var responseDTO = await HttpClient.DeleteFromJsonAsync<ResponseDTO<object>>($"api/addresses/{addressDTO.Id}");
                return responseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        public async Task<ResponseDTO<object>> setDefaultAsync(int addressId)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var response = await HttpClient.PostAsJsonAsync<int>($"api/addresses/setDefault", addressId);
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
