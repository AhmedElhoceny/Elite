using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }
        public string fromUserUserName { get; set; }
        public string CommentContent { get; set; }
        public string TaskTitle { get; set; }
        public DateTime CommentTime { get; set; }
    }
}
