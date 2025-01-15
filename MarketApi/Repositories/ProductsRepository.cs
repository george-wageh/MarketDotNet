using MarketApi.Data;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.Services;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.IRepositories
{
    public class ProductsRepository : IProductsRepository
    {
        public ApplicationDbContext ApplicationDbContext { get; }

        public ProductsRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Product product)
        {
            await this.ApplicationDbContext.AddAsync(product);
        }

        public void Delete(Product product)
        {
            this.ApplicationDbContext.Remove(product);
        }

        public IQueryable<Product> GetAll()
        {
            return ApplicationDbContext.Set<Product>().Where(x=>true);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await ApplicationDbContext.Set<Product>().FindAsync(id);
        }

        public void Update(Product product)
        {
            ApplicationDbContext.Update<Product>(product);
        }
        public async Task<bool> ExistsProductAsync(int productId)
        {
            return await ApplicationDbContext.Set<Product>().AnyAsync(x => x.Id == productId);
        }

        public IQueryable<Product> GetAllInCategoryAsync(int CategoryId)
        {
            //throw new NotImplementedException();
            return ApplicationDbContext.Set<Product>().Where(x=>x.CategoryId == CategoryId);
        }

        public IQueryable<Product> SearchInName(string Qstring)
        {
            return ApplicationDbContext.Set<Product>().Where(x => x.Name.Contains(Qstring));
        }

        public IQueryable<Product> GetEmptyIQueryable()
        {
            return ApplicationDbContext.Set<Product>().Where(x => false);

        }
    }
}
