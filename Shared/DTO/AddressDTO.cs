using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone must be 11 characters long.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$",
            ErrorMessage = "Phone number must start with 010, 011, 012, or 015 and contain 11 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Street Address is required.")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Postal Code must be a 5-digit number.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        public bool IsDefault { get; set; }

        public AddressDTO Clone() {
            return new AddressDTO
            {
                City = this.City,
                Country = this.Country,
                StreetAddress = this.StreetAddress,
                FullName = this.FullName,
                Id = this.Id,
                IsDefault = this.IsDefault,
                PostalCode = this.PostalCode,
                Phone = this.Phone
            };
        }
    }
}
