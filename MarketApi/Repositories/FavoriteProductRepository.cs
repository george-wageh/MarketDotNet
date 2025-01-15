using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;
using static MarketApi.Models.FavoriteProducts;

namespace MarketApi.Repositories
{
    public class FavoriteProductRepository : IFavoriteProductRepository
    {
        public FavoriteProductRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }
        public async Task<bool> IsProductFavoritedAsync(string userId, int productId)
        {
            return await ApplicationDbContext.FavoriteProducts.AnyAsync(fp => fp.UserId == userId && fp.ProductId == productId);
        }
        public async Task<IEnumerable<Product>> GetAllFavListAsync(string userId) {
            return await this.ApplicationDbContext.Set<FavoriteProduct>()
                .Where(x => x.UserId == userId)
                .Include(x => x.Product)
                .Select(x => x.Product).ToListAsync();
        }
        public async Task AddToFavorite(int productId, string UserId)
        {
            var favoriteProduct = new FavoriteProduct { UserId = UserId, ProductId = productId };
            await ApplicationDbContext.FavoriteProducts.AddAsync(favoriteProduct);
        }

        public async Task<IEnumerable<FavoriteProduct>> GetFavorites(string UserId)
        {
            return await ApplicationDbContext.FavoriteProducts.Where(x=>x.UserId == UserId).ToListAsync();
        }

        public void Remove(int productId, string UserId)
        {
            var favoriteProduct = new FavoriteProduct { UserId = UserId, ProductId = productId };
            ApplicationDbContext.FavoriteProducts.Remove(favoriteProduct);
        }

        public IQueryable<int> GetAllProductIdsFavQueryable(string userId)
        {
            //throw new NotImplementedException();

            return ApplicationDbContext.Set<FavoriteProduct>().
                Where(x => x.UserId == userId).Select(x=>x.ProductId);
        }
    }
}
