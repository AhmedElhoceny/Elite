using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Rank { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public int Excuses { get; set; }
        public int AnualBreaks { get; set; }
    }
}
