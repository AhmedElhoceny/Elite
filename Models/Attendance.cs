using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Models
{
    public class Attendance
    {
        [Key]
        public int id { get; set; }
        public int UserID { get; set; }
        public DateTime DayStartTime { get; set; }
        public DateTime GetAwayTime { get; set; }
        public DateTime SouldGetAwwayTime { get; set; }
    }
}
