using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Models
{
    public class Task
    {
        [Key]
        public int id { get; set; }
        public string TaskTitle { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TaskType { get; set; }
        public string TaskDiscription { get; set; }
        public string TaskState { get; set; }
        public string FinalState { get; set; }
        public bool Argent { get; set; }
    }
}
