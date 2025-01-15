using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class OrderCardDTO
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public decimal ShipPrice { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<string> productImages { get; set; }
    }
}
