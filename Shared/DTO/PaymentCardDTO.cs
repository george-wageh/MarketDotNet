using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class PaymentCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Last4digits { get; set; }
        public DateOnly ExpDate { get; set; }
        public bool IsDefault { get; set; }

    }
}
