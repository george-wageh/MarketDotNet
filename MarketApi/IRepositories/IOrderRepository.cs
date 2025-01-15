using MarketApi.Models;

namespace MarketApi.IRepositories
{
    public interface IOrderRepository
    {
        public Task<Order> GetOrderAsync(string userId, int id);

        public Task<Order> AddOrderAsync(Order order);

        public Task<IEnumerable<Order>> GetAllAsync(string userId);


    }
}
