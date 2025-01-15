using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class ResponseListDTO<T> : ResponseDTO<T>
    {
        public int Count { get; set; }
    }
}
