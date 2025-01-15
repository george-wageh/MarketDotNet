using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
 
        [StringLength(100, ErrorMessage = "Cardholder Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Card Number is required.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card Number must be 16 digits.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Expiration Date.")]
        public DateOnly ExpDate { get; set; }

        [Required(ErrorMessage = "CVV Code is required.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV Code must be 3 or 4 digits.")]
        public string CVVCode { get; set; }

        public bool IsDefault { get; set; }
    }
}
