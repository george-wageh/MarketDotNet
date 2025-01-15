using MarketApi.Models;

namespace MarketApi.IRepositories
{
    public interface IAddressRepository
    {
        public Task AddAsync(Address address);
        public Task<IEnumerable<Address>> GetAllAsync(string userId);
        public Task<Address> GetAsync(int id);
        public void Delete(Address address);
        public void Edit(Address address);
        public Task<bool> IsFirstAddressAsync(string userId);
        public Task<Address> GetDefaultAsync(string userId);


    }
}
