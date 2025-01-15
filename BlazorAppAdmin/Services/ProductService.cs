using BlazorAppAdmin.Shared;
using Microsoft.AspNetCore.Authorization;
using SharedLib.DTO;
using System.Net;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class ProductService
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }
        // Get a list of all products
        public async Task<ResponseListDTO<IEnumerable<ProductCardDTO>>> GetProductsInQuery(ProductQueryDTO productQueryDTO)
        {
            try
            {
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");

                var x = await HttpClient.PostAsJsonAsync("api/AdminProduct/GetProductsInQuery", productQueryDTO);
                var response = await x.Content.ReadFromJsonAsync<ResponseListDTO<IEnumerable<ProductCardDTO>>>();
                return response;

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
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<ProductDTO>>($"api/AdminProduct/{productId}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        [Authorize(Roles = "Admin")]
        // Get a list of all products
        public async Task<ResponseDTO<ProductDTO>> AddAsync(MultipartFormDataContent image, ProductDTO product)
        {
            HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
            if (image != null) {
                try
                {

                    var x = await HttpClient.PostAsync("api/AdminProduct/PostImage", image);
                    var response = await x.Content.ReadFromJsonAsync<ResponseDTO<string>>();
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            product.ImageUrl = response.Data;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                    return null;
                }
            }
           
            try
            {
                var x = await HttpClient.PostAsJsonAsync("api/AdminProduct", product);
                return await x.Content.ReadFromJsonAsync<ResponseDTO<ProductDTO>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        [Authorize(Roles = "Admin")]
        // Get a list of all products
        public async Task<ResponseDTO<object>> EditAsync(MultipartFormDataContent image, ProductDTO product)
        {
            HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
            //product.ImageUrl = "";
            if (image != null)
            {
                try
                {

                    var x = await HttpClient.PostAsync("api/products/PostImage", image);
                    var response = await x.Content.ReadFromJsonAsync<ResponseDTO<string>>();
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            product.ImageUrl = response.Data;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                    return null;
                }
            }

            try
            {
                var x = await HttpClient.PutAsJsonAsync($"api/AdminProduct", product);
                return await x.Content.ReadFromJsonAsync<ResponseDTO<object>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<ResponseDTO<object>> DeleteAsync(int productId) { 
            HttpClient HttpClient = HttpClientFactory.CreateClient("Client");
            var response = await HttpClient.DeleteAsync($"api/AdminProduct/{productId}");
            var responseDto = await response.Content.ReadFromJsonAsync<ResponseDTO<object>>();
            return responseDto;
        }
    }
}
