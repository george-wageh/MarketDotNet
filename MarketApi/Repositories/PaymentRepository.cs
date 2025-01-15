using MarketApi.Data;
using MarketApi.IRepositories;
using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public async Task AddPaymentCard(Payment payment)
        {
            await ApplicationDbContext.AddAsync(payment);
        }

        public void DeletePaymentCard(Payment payment)
        {
            ApplicationDbContext.Remove(payment);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync(string userId)
        {
           return await ApplicationDbContext.Set<Payment>().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Payment> GetAsync(string userId, int id)
        {
            //throw new NotImplementedException();
            var payment = await ApplicationDbContext.Set<Payment>().Where(x => x.UserId == userId && x.Id == id).FirstOrDefaultAsync();
            return payment;
        }

        public async Task<Payment> GetDefaultAsync(string userId)
        {
            return await ApplicationDbContext.Set<Payment>().Where(x => x.UserId == userId && x.IsDefault == true).FirstOrDefaultAsync();
        }

        public async Task<bool> IsFirstPaymentCardAsync(string userId)
        {
            return !(await ApplicationDbContext.Set<Payment>().AnyAsync(x => x.UserId == userId));

        }
    }
}
