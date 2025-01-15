using MarketApi.Models;
using static MarketApi.Models.FavoriteProducts;

namespace MarketApi.Repositories
{
    public interface IFavoriteProductRepository
    {
        public Task<bool> IsProductFavoritedAsync(string userId, int productId);
        public Task AddToFavorite(int productId, string UserId);
        public Task<IEnumerable<FavoriteProduct>> GetFavorites(string UserId);
        public void Remove(int productId, string UserId);
        public Task<IEnumerable<Product>> GetAllFavListAsync(string userId);
        public IQueryable<int> GetAllProductIdsFavQueryable(string userId);



    }
}
