using MarketApi.Models;

namespace MarketApi.IRepositories
{
    public interface IUserRepository
    {
        public IQueryable<ApplicationUser> GetEmptyQueryable();
        public IQueryable<ApplicationUser> SearchByPhone(string phone);
        public IQueryable<ApplicationUser> SearchByEmail(string email);
        public IQueryable<ApplicationUser> SearchByUsername(string userName);
        public IQueryable<ApplicationUser> GetAllUsers();

    }
}
