using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task<Order> AddOrderAsync(Order order) {
            await ApplicationDbContext.Set<Order>().AddAsync(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string userId)
        {
            return await ApplicationDbContext.Set<Order>()
                 .Where(x => x.UserId == userId)
                 .Include(x => x.OrderProducts)
                     .ThenInclude(op => op.Product) // Include the whole Product
                 .OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<Order> GetOrderAsync(string userId, int id)
        {
           return await ApplicationDbContext.Set<Order>().Where(x=>x.Id == id).
                Include(x=>x.OrderProducts).ThenInclude(x=>x.Product).
                Include(x=>x.OrderStates).Include(x=>x.OrderAddress).Include(x=>x.OrderPayment).FirstOrDefaultAsync();

        }
    }
}
