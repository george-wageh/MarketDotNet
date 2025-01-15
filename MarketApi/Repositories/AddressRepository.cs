using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<Address> GetAsync(int id)
        {
            return await ApplicationDbContext.Set<Address>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task AddAsync(Address address)
        {
            await ApplicationDbContext.Set<Address>().AddAsync(address);
        }
        public void Delete(Address address)
        {
            ApplicationDbContext.Set<Address>().Remove(address);
        }

        public void Edit(Address address)
        {
            ApplicationDbContext.Set<Address>().Update(address);
        }

        public async Task<IEnumerable<Address>> GetAllAsync(string userId)
        {
            return await ApplicationDbContext.Set<Address>().Where(x=>x.UserId == userId).ToListAsync();
        }

        public async Task<bool> IsFirstAddressAsync(string userId) {
            return !(await ApplicationDbContext.Set<Address>().AnyAsync(x => x.UserId == userId));
        }
        public async Task<Address> GetDefaultAsync(string userId)
        {
            return await ApplicationDbContext.Set<Address>().Where(x => x.UserId == userId && x.IsDefault == true).FirstOrDefaultAsync();
        }

    }
}
