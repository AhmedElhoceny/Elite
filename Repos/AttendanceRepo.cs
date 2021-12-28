using Elite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Repos
{
    public class AttendanceRepo : GeneralInterface<Attendance>
    {
        public DBCoClass MyDb { get; set; }
        public AttendanceRepo(DBCoClass MyDb)
        {
            this.MyDb = MyDb;
        }
        public void AddNewItem(Attendance NewItem)
        {
            MyDb.Attendances.Add(NewItem);
        }

        public void DeleteItem(int id)
        {
            var SearhcedAttendance = MyDb.Attendances.ToList().Where(x => x.id == id).FirstOrDefault();
            MyDb.Attendances.Remove(SearhcedAttendance);
        }

        public void EditItem(int id, Attendance UpdateData)
        {
            var SearhcedAttendance = MyDb.Attendances.ToList().Where(x => x.id == id).FirstOrDefault();
            MyDb.Attendances.Update(SearhcedAttendance);
        }

        public List<Attendance> GetAllData()
        {
            return MyDb.Attendances.ToList();
        }
        public void LogInAttendance(string UserName)
        {
            MyDb.Attendances.Add(new Attendance()
            {
                DayStartTime = DateTime.Now,
                GetAwayTime = DateTime.Now,
                UserID = MyDb.Users.Where(x => x.UserName == UserName).FirstOrDefault().id,
                SouldGetAwwayTime = DateTime.Now
            });
        }
        public List<Attendance> GetAllUserAttendanceByUserId(int UserId)
        {
            return MyDb.Attendances.ToList().Where(x => x.UserID == UserId).ToList();
        }
        public void LogOutAttendance(string UserName)
        {
            var SearchedUser = MyDb.Users.Where(x => x.UserName == UserName).FirstOrDefault();
            var SearchedAttendance = MyDb.Attendances.ToList().Where(x => x.UserID == SearchedUser.id && x.GetAwayTime.Hour == x.DayStartTime.Hour && x.GetAwayTime.Minute == x.DayStartTime.Minute).FirstOrDefault();
            SearchedAttendance.GetAwayTime = DateTime.Now;
            EditItem(SearchedAttendance.id, SearchedAttendance);
        }
        public List<Attendance> GetLoggingTodayByUserId(int UserId)
        {
            return MyDb.Attendances.ToList().Where(x => x.UserID == UserId && x.DayStartTime.Day == DateTime.Now.Day).ToList();
        }
    }
}
