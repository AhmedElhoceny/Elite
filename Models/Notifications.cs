using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Models
{
    public class Notifications
    {
        [Key]
        public int id { get; set; }
        public string Notif_From { get; set; }
        public string Notif_To { get; set; }
        public string Message { get; set; }
        public bool State { get; set; }
    }
}
