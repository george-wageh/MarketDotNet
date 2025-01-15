using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string city { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string Phone { get; set; }

        public bool IsDefault { get; set; }
    }
}
