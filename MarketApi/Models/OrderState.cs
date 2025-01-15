using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class OrderState
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int Date { get; set; }

        public byte OrderStatus { get; set; }
    }
}
