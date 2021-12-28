using Elite.Models;
using Elite.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserRepo userFuns;
        private readonly CommentRepo commentFuns;
        private readonly TaskRepo taskFuns;
        private readonly AttendanceRepo attendanceFuns;
        private readonly AccessDataBaseChanges AccessDB;
        public AdminController(UserRepo userFuns, CommentRepo commentFuns, TaskRepo taskFuns, AttendanceRepo attendanceFuns, AccessDataBaseChanges AccessDB)
        {
            this.userFuns = userFuns;
            this.commentFuns = commentFuns;
            this.taskFuns = taskFuns;
            this.attendanceFuns = attendanceFuns;
            this.AccessDB = AccessDB;
        }
        public ActionResult WelcomeAdmin()
        {
            List<User> OurUsers = userFuns.GetAllData();
            return View(OurUsers);
        }
        public string AddUser(string FirstName , string LastName , string Team , string Position , string Email , string PassWord)
        {
            try
            {
                var UserName = FirstName + LastName;
                var AllUsers =userFuns.GetAllData();
                foreach (var AllUsersItem in AllUsers)
                {
                    if (AllUsersItem.Email == Email || AllUsersItem.UserName == UserName || AllUsersItem.Position == null || AllUsersItem.Team == null || AllUsersItem.Password == null)
                    {
                        return "Failed";
                    }
                }
                userFuns.AddNewItem(new User()
                {
                    AnualBreaks = 23,
                    Email = Email,
                    Excuses = 3,
                    Password = PassWord,
                    UserName = UserName,
                    Position = Position,
                    Rank = 100,
                    Team = Team
                });
                AccessDB.AccessChanges();
                return "Done";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        public ActionResult EditUser(string UserName, string Email, string UserTeam, string UserPosition)
        {
            try
            {
                if (UserName == null || Email == null || UserTeam == null || UserPosition == null)
                {
                    return Redirect("/Home/Index");
                }
                var SearchedUser = userFuns.FindByUserName(UserName);
                SearchedUser.Email = Email;
                SearchedUser.Team = UserTeam;
                SearchedUser.Position = UserPosition;
                userFuns.EditItem(SearchedUser.id, SearchedUser);
                AccessDB.AccessChanges();
                return RedirectToAction(nameof(WelcomeAdmin));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult RemoveUser(string UserName)
        {
            try
            {
                if (UserName == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                var SearchedUser = userFuns.FindByUserName(UserName);
                userFuns.DeleteItem(SearchedUser.id);
                AccessDB.AccessChanges();
                return RedirectToAction(nameof(WelcomeAdmin));
            }
            catch (Exception)
            {
                return Redirect("/Home/Index");
            }
        }
        public ActionResult ShowOfficersDataTasks(string OfficerName)
        {
            var SearchedUser = userFuns.FindByUserName(OfficerName);
            var OurOfficerTasks = taskFuns.FindUserTasksByUserId(SearchedUser.id);
            GeneralModelView ViewData = new GeneralModelView();
            ViewData.User = SearchedUser;
            ViewData.UserTasks = OurOfficerTasks;
            ViewData.TaskComments = new List<Comment>();
            foreach (var OurOfficerTasksItem in OurOfficerTasks)
            {
                ViewData.TaskComments.AddRange(commentFuns.GetAllTaskComments(OurOfficerTasksItem.TaskTitle));
            }
            return View(ViewData);
        }
        public ActionResult GetAttendanceData()
        {
            int Counter = 0;
            List<AttendanceViewModel> Data = new List<AttendanceViewModel>();
            var Users = userFuns.GetAllData();
            foreach (var User in Users)
            {
                Counter += 1;
                decimal GeneralHours = 0;
                decimal GeneralMinutes = 0;
                decimal GeneralSeconds = 0;
                //myDB.Attendance.ToList().Where(x => x.UserID == User.id && x.DayStartTime.Day == DateTime.Now.Day).ToList();
                var AllTodayAttendances = attendanceFuns.GetLoggingTodayByUserId(User.id);
                foreach (var AttendanceItem in AllTodayAttendances)
                {
                    GeneralHours += Math.Abs(AttendanceItem.GetAwayTime.Hour - AttendanceItem.DayStartTime.Hour);
                    GeneralMinutes += Math.Abs(AttendanceItem.GetAwayTime.Minute - AttendanceItem.DayStartTime.Minute);
                    GeneralSeconds += Math.Abs(AttendanceItem.GetAwayTime.Second - AttendanceItem.DayStartTime.Second);
                }
                Data.Add(new AttendanceViewModel()
                {
                    Counter = Counter,
                    UserName = User.UserName,
                    WorkingHours = GeneralHours,
                    WorkingMinutes = GeneralMinutes,
                    WorkingSeconds = GeneralSeconds
                });
            }
            return View(Data);
        }
    }
}
