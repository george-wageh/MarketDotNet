using MarketApi.Models;

namespace MarketApi.IRepositories
{
    public interface ICategoryRepository
    {
        public Task AddAsync(Category category);
        public void EditAsync(Category category);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> GetAsync(int Id);
        public Task<bool> IsExistAsync(int Id);
        public Task<List<int>> GetCategoryAndChildrenIds(int categoryId);
        public Task<Category> GetRootAsync();
    }
}
