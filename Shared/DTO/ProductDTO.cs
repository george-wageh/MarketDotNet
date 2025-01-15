using System.ComponentModel.DataAnnotations;

namespace SharedLib.DTO
{
    public class ProductDTO:ProductCardDTO
    {
        [Required , MinLength(10)]
        public string Description { get; set; }
    }
}
