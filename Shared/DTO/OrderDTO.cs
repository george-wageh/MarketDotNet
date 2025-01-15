using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public decimal ShipPrice { get; set; }

        public DateTime CreationDate { get; set; }

        public AddressDTO Address { get; set; }

        public PaymentCardDTO Payment { get; set; }

        public IEnumerable<ProductQuantityCardDTO> Products { get; set; }

        public IEnumerable<OrderStateDTO> OrderStates { get; set; }
        
    }
}
