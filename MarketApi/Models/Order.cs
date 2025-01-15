using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [InverseProperty(nameof(OrderAddress))]
        public int OrderAddressId { get; set; }
        public OrderAddress OrderAddress { get; set; }

        [InverseProperty(nameof(OrderPayment))]
        public int OrderPaymentId { get; set; }
        public OrderPayment OrderPayment { get; set; }


        public decimal Total { get; set; }
        public decimal ShipPrice { get; set; }
        public DateTime CreationDate { get; set; }


        public ICollection<OrderProduct> OrderProducts { get; set; }

        public ICollection<OrderState> OrderStates { get; set; }


    }
}
