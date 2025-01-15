using SharedLib.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorAppAdmin.Services
{
    public class CommentsService
    {
        public CommentsService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public async Task<ResponseDTO<IEnumerable<CommentDTO>>> GetCommentsProduct(int productId) {
            try
            {
                HttpClient HttpClient = HttpClientFactory.CreateClient("Client");

                var response = await HttpClient.GetFromJsonAsync<ResponseDTO<IEnumerable<CommentDTO>>>($"/api/comments/{productId}");
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
