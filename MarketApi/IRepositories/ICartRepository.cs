using MarketApi.Models;

namespace MarketApi.IRepositories
{
    public interface ICartRepository
    {
        public Task AddToCartAsync(CartProduct cartProduct);
        public void Delete(CartProduct cartProduct);
        public Task<CartProduct?> GetProductAsync(string userId, int productId);
        public Task<IEnumerable<CartProduct>> GetCartProductsAsync(string userId );
        public void DeleteAllCart(IEnumerable<CartProduct> CartProducts);
    }
}
