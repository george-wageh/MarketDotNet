using MarketApi.Models;
using MarketApi.Services;

namespace MarketApi.Repositories
{
    public interface IProductsRepository
    {
        public Task AddAsync(Product product);
        public void Delete(Product product);
        public IQueryable<Product> GetAll();
        public IQueryable<Product> GetAllInCategoryAsync(int CategoryId);
        public IQueryable<Product> SearchInName(string Qstring);
        public IQueryable<Product> GetEmptyIQueryable();
        public Task<Product> GetByIdAsync(int id);
        public void Update(Product product);
        public Task<bool> ExistsProductAsync(int productId);

    }
}
