using MarketApi.Data;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.UnitWork
{
    public class UnitWorkApp
    {
        public UnitWorkApp(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task SaveChangesAsync() { 
           await ApplicationDbContext.SaveChangesAsync();
        }

        public async Task<string?> GetUserId(string userName)
        {
           var user = await ApplicationDbContext.Set<ApplicationUser>().Select(x => new { x.UserName , x.Id}).FirstOrDefaultAsync(x => x.UserName == userName);
            if (user != null) { 
                return user.Id;
            }
            return null;
        }


    }
}
