using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class ProductQueryDTO
    {
        public int? CategoryId { get; set; }
        public int? Count { get; set; }
        public string? SortBy { get; set; }
        public int? PageNum { get; set; }
        public string? Qstring { get; set; }
    }
}
