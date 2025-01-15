using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class OrderAddress
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }


        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string city { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string Phone { get; set; }
    }
}
