using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class OrderPayment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }


        public string Name { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpDate { get; set; }
        public string CVVCode { get; set; }
    }
}
