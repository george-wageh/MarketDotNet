using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.AdminDTO
{
    public class UserQueryDTO
    {
        public string? SearchBy { get; set; }
        public string? Qstring { get; set; }
        public int? Count { get; set; }
        public string? HasOrder { get; set; }
        public int? PageNum { get; set; }
    }
}
