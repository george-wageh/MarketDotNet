using SharedLib.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class ProductService
    {

        public IHttpClientFactory HttpClientFactory { get; }

        public ProductService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }


        public async Task<ResponseListDTO<IEnumerable<ProductCardDTO>>> GetProductsInQuery(ProductQueryDTO productQueryDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

                var x = await HttpClient.PostAsJsonAsync<ProductQueryDTO>("api/products/GetProductsInQuery", productQueryDTO);
                return await x.Content.ReadFromJsonAsync<ResponseListDTO<IEnumerable<ProductCardDTO>>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }




        // Get a list of all products
        public async Task<ResponseDTO<ProductDTO>> GetProductAsync(int productId)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var x = await HttpClient.GetFromJsonAsync<ResponseDTO<ProductDTO>>($"api/products/{productId}");
                return x;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        // Get a list of all products
        public async Task<ResponseDTO<ProductDTO>> AddAsync(ProductDTO productDTO)
        {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");
                var x = await HttpClient.PostAsJsonAsync("api/products" , productDTO);
                return await x.Content.ReadFromJsonAsync<ResponseDTO<ProductDTO>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

    }
}
