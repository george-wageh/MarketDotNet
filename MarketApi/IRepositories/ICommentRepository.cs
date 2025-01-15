using MarketApi.Models;

namespace MarketApi.Repositories
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetAllWithUsersAsync(int productId);
        public Task AddAsync(Comment comment);
        public Task<Comment> GetAsync(int commentId);
        public void Delete(Comment comment);
    }
}
