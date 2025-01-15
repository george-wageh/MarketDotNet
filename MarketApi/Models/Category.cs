namespace MarketApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public Category Parent { get; set; }
        public ICollection<Category> Children { get; set; }

        // Association with Products
        public ICollection<Product> Products { get; set; }
    }
}
