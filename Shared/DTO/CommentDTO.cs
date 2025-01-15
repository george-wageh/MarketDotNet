using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
    }
}
