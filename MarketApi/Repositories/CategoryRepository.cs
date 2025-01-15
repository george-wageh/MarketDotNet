using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task AddAsync(Category category) {
            await ApplicationDbContext.Set<Category>().AddAsync(category);
        }

        public void EditAsync(Category category)
        {
            ApplicationDbContext.Set<Category>().Update(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            //throw new NotImplementedException();
            return await ApplicationDbContext.Set<Category>().ToListAsync();
        }

        public async Task<Category> GetAsync(int Id)
        {
            //throw new NotImplementedException();
            return await ApplicationDbContext.Set<Category>().Where(x => x.Id == Id).FirstOrDefaultAsync();

        }

        public async Task<List<int>> GetCategoryAndChildrenIds(int categoryId)
        {
            var productsIds = new List<int>();
            var categoriesToProcess = new Queue<int>(); // Queue to process categories iteratively
            categoriesToProcess.Enqueue(categoryId); // Start with the root category

            while (categoriesToProcess.Count > 0)
            {
                // Get the next category to process
                var currentCategoryId = categoriesToProcess.Dequeue();

                // Fetch the current category with its products and children
                var currentCategory = await ApplicationDbContext.Set<Category>()
                    .Include(c => c.Children)
                    .FirstOrDefaultAsync(c => c.Id == currentCategoryId);

                if (currentCategory == null)
                    continue;

                // Add id from the current category
                productsIds.Add(currentCategory.Id);

                // Enqueue child categories to process them later
                foreach (var child in currentCategory.Children)
                {
                    categoriesToProcess.Enqueue(child.Id);
                }
            }

            return productsIds;
        }

        public async Task<Category> GetRootAsync()
        {
            return await ApplicationDbContext.Set<Category>().Where(_ => _.ParentId == null).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistAsync(int Id)
        {
            return await ApplicationDbContext.Set<Category>().AnyAsync(_ => _.Id == Id);
        }
    }
}
