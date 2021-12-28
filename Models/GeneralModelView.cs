using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Models
{
    public class GeneralModelView
    {
        public User User { get; set; }
        public List<Task> UserTasks { get; set; }
        public List<Attendance> UserAttendance { get; set; }
        public List<User> AllUsers { get; set; }
        public List<Comment> TaskComments { get; set; }
        public List<Notifications> MyNotifications { get; set; }

    }
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
    }
    public class AttendanceViewModel
    {
        public int Counter { get; set; }
        public string UserName { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal WorkingMinutes { get; set; }
        public decimal WorkingSeconds { get; set; }
    }
}
