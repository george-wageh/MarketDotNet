using MarketApi.Models;

namespace MarketApi.IRepositories
{
    public interface IPaymentRepository
    {
        public Task AddPaymentCard(Payment payment);
        public void DeletePaymentCard(Payment payment);
        public Task<bool> IsFirstPaymentCardAsync(string userId);
        public Task<Payment> GetAsync(string userId , int id);
        public Task<IEnumerable<Payment>> GetAllAsync(string userId );
        public Task<Payment> GetDefaultAsync(string userId );

    }
}
