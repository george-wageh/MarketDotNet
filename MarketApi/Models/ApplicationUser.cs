using Microsoft.AspNetCore.Identity;
using static MarketApi.Models.FavoriteProducts;

namespace MarketApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
