using SharedLib.AdminDTO;
using SharedLib.DTO;
using System.Net.Http.Json;

namespace BlazorAppUser.Services
{
    public class CommentsService
    {
        public CommentsService(IHttpClientFactory HttpClientFactory)
        {
            this.HttpClientFactory = HttpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }


        public async Task<ResponseDTO<IEnumerable<CommentDTO>>> GetCommentsProduct(int productId) {
            try
            {
                var HttpClient = HttpClientFactory.CreateClient("Client");

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
