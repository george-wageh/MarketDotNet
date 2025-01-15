using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Comment> comments { get; set; }

        // Foreign Key to Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public string Image { get { return "https://localhost:7247/" + this.ImageUrl; } }
    }

}
