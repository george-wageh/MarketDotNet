using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext )
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }
        public IUserRoleStore<ApplicationUser> UserRoleStore { get; }

    

        //public Get
        public IQueryable<ApplicationUser> GetEmptyQueryable()
        {
           return GetAllUsers().Where(x => false);
        }

        public IQueryable<ApplicationUser> SearchByEmail(string email)
        {
            return GetAllUsers().Where(x => x.Email == email);

        }

        public IQueryable<ApplicationUser> SearchByPhone(string phone)
        {
            return GetAllUsers().Where(x => x.PhoneNumber == phone);

        }

        public IQueryable<ApplicationUser> SearchByUsername(string userName)
        {
            return GetAllUsers().Where(x => x.UserName == userName);
        }
        public IQueryable<ApplicationUser> GetAllUsers()
        {
            var role = ApplicationDbContext.Roles.FirstOrDefault(r => r.Name == "User");
            return ApplicationDbContext.Users
                .Where(user => ApplicationDbContext.UserRoles.Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id));
        }

    }
}
