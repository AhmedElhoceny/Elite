using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Elite.Models
{
    public class DBCoClass : DbContext
    {
        public DBCoClass(DbContextOptions<DBCoClass> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Notifications> Notification { get; set; }
    }
}
