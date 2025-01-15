using SharedLib.DTO;

namespace MarketApi.DTO
{
    public class ProductImageDTO:ProductDTO
    {
        public IFormFile image { get; set; }
    }
}
