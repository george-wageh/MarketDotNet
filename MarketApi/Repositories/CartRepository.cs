using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Repositories
{
    public class CartRepository: ICartRepository
    {
        public CartRepository(ApplicationDbContext applicationDbContext )
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }


        public async Task AddToCartAsync(CartProduct cartProduct) {
            await this.ApplicationDbContext.AddAsync(cartProduct);
        }

        public void Delete(CartProduct cartProduct)
        {
            this.ApplicationDbContext.Remove(cartProduct);
        }



        public async Task<CartProduct?> GetProductAsync(string userId, int productId)
        {
           return await this.ApplicationDbContext.Set<CartProduct>().Where(x=>(x.UserId == userId && x.ProductId == productId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CartProduct>> GetCartProductsAsync(string userId)
        {
            return await this.ApplicationDbContext.Set<CartProduct>().Where(x => x.UserId == userId).Include(x=>x.Product).ToListAsync();
        }

        public void DeleteAllCart(IEnumerable<CartProduct> CartProducts)
        {
            this.ApplicationDbContext.Set<CartProduct>().RemoveRange(CartProducts);
        }
    }
}
