using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using MarketApi.Services;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public CommentRepository(ApplicationDbContext applicationDbContext) 
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task AddAsync(Comment comment)
        {
            await ApplicationDbContext.Set<Comment>().AddAsync(comment);
        }

        public void Delete(Comment comment)
        {
            ApplicationDbContext.Set<Comment>().Remove(comment);
        }

        public async Task<Comment> GetAsync(int commentId)
        {
            //throw new NotImplementedException();
            return await ApplicationDbContext.Set<Comment>().FirstOrDefaultAsync(comment => comment.Id == commentId);
        }

        public async Task<IEnumerable<Comment>> GetAllWithUsersAsync(int productId)
        {
            // Fetch comments with user data in one query
            return await ApplicationDbContext.Set<Comment>()
                                   .Include(x => x.User) // Eager load User data
                                   .Where(x => x.ProductId == productId)
                                   .ToListAsync();
        }
    }
}
