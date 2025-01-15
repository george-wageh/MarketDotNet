using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public string Name { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpDate { get; set; }
        public string CVVCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
