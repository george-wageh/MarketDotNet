using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class CartProduct
    {
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
