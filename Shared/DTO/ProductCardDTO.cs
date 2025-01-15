
using System.ComponentModel.DataAnnotations;

namespace SharedLib.DTO
{
    public class ProductCardDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public bool IsFav { get; set; }
    }
}
