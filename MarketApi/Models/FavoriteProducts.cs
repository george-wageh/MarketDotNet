namespace MarketApi.Models
{
    public class FavoriteProducts
    {
        public class FavoriteProduct
        {
            public string UserId { get; set; }
            public ApplicationUser User { get; set; }

            public int ProductId { get; set; }
            public Product Product { get; set; }
        }
    }
}
