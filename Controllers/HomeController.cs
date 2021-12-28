using Elite.Models;
using Microsoft.AspNetCore.Hosting;
using Elite.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task = Elite.Models.Task;
using Elite.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Elite.Controllers
{

    public class HomeController : Controller
    {

        private readonly UserRepo userFuns;
        private readonly CommentRepo commentFuns;
        private readonly TaskRepo taskFuns;
        private readonly AttendanceRepo attendanceFuns;
        private readonly AccessDataBaseChanges AccessDB;
        private readonly IHubContext<CommentHub> _hubContext;
        private readonly NotificationsRepo notificationFuns;

        public HomeController(UserRepo userFuns , CommentRepo commentFuns , TaskRepo taskFuns, AttendanceRepo attendanceFuns , AccessDataBaseChanges AccessDB , IHubContext<CommentHub> hubContext , NotificationsRepo NotificationFuns)
        {
           this.userFuns = userFuns;
            this.commentFuns = commentFuns;
            this.taskFuns = taskFuns;
            this.attendanceFuns = attendanceFuns;
            this.AccessDB = AccessDB;
            _hubContext = hubContext;
            notificationFuns = NotificationFuns;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAttendance(string UserName, string Password)
        {
            if (UserName == "admin" && Password == "admin")
            {
                return Redirect("../Admin/WelcomeAdmin");
            }
            string IsClientExist = userFuns.SearchUser(UserName, Password);
            if (IsClientExist == "Yes")
            {
                attendanceFuns.LogInAttendance(UserName);
                AccessDB.AccessChanges();
                return RedirectToAction(nameof(LogInPost), new { UserNmae = UserName, Password = Password });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }    
        }
        public ActionResult LogInPost(string UserNmae, string Password)
        {
            
            var SearchedUser = userFuns.FindByUserName(UserNmae);
            
            if (SearchedUser == null)
            {
                return RedirectToAction(nameof(Index));
            }

            GeneralModelView Data = new GeneralModelView();
            Data.User = SearchedUser;
            Data.UserAttendance = attendanceFuns.GetAllUserAttendanceByUserId(SearchedUser.id);
            Data.UserTasks = taskFuns.GetAllUserTasks(SearchedUser.id);
            Data.AllUsers = userFuns.GetAllData();
            Data.MyNotifications = notificationFuns.GetAllMyNotifications(UserNmae);

            ViewBag.InprogressTasks = taskFuns.GetAllInProgressTasksPerUserId(SearchedUser.id).Count();
            ViewBag.ClosedTasks = taskFuns.GetAllClosedTasksPerUserId(SearchedUser.id).Count();
            ViewBag.AllDailyTasks = taskFuns.GetAllDailyTasksPerUserId(SearchedUser.id).Count();
            var AllDailyTasksDone = taskFuns.GetAllDailyDoneTasksPerUserId(SearchedUser.id).Count();
            if (ViewBag.AllDailyTasks == 0)
            {
                ViewBag.DilyStatistics = 0;
            }
            else
            {
                ViewBag.DilyStatistics = (AllDailyTasksDone / ViewBag.AllDailyTasks) * 100;
            }
            ViewBag.AlllONGtERMTasks = taskFuns.GetAlllongtermTasksPerUserId(SearchedUser.id).Count();
            var AlllONGtERMTasksDone = taskFuns.GetAllLongTemsDoneTasksPerUserId(SearchedUser.id).Count();
            if (ViewBag.AlllONGtERMTasks == 0)
            {
                ViewBag.LongTermStatistics = 0;
            }
            else
            {
                ViewBag.LongTermStatistics = (AlllONGtERMTasksDone / ViewBag.AlllONGtERMTasks) * 100;
            }
            ViewBag.AllSupportiveTasks = taskFuns.GetAllsupportiveTasksPerUserId(SearchedUser.id).Count();
            var AllSupportiveTasksDone = taskFuns.GetAllSupportiveTasksDonePerUserId(SearchedUser.id).Count();
            if (ViewBag.AllSupportiveTasks == 0)
            {
                ViewBag.SupportiveStatistics = 0;
            }
            else
            {
                ViewBag.SupportiveStatistics = (AllSupportiveTasksDone / ViewBag.AllSupportiveTasks) * 100;
            }

            return View("LogInPost", Data);
        }
        public ActionResult AddTask(string SelectedPerson, string SelectedClient, string URL, string TaskType, string TaskPlateForm, string Discription, string EndDate = "", string TaskTitle = "", int fromUserId = 0, int TaskDailyTime = 0, bool argent = false)
        {
            var SearchedUserFrom = userFuns.FindByUserId(fromUserId);
            var AllTasks = taskFuns.GetAllData();
            foreach (var TaskItem in AllTasks)
            {
                if (TaskItem.TaskTitle == TaskTitle)
                {
                    return RedirectToAction(nameof(LogInPost), new { UserNmae = SearchedUserFrom.UserName, Password = SearchedUserFrom.Password });
                }
            }
            DateTime EndDateComplete = new DateTime();
            if (EndDate != "" && EndDate != null)
            {
                var Year = int.Parse(EndDate.Substring(0, 4));
                var Month = int.Parse(EndDate.Substring(5, 2));
                var Day = int.Parse(EndDate.Substring(8, 2));
                EndDateComplete = new DateTime(Year, Month, Day);
            }
            else if (TaskDailyTime != 0)
            {
                EndDateComplete = DateTime.Now;
                EndDateComplete.AddHours(TaskDailyTime);
            }
            else
            {
                EndDateComplete = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0);
            }
            var SearchedTaskTo = userFuns.FindByUserName(SelectedPerson);
            if (SearchedTaskTo != null)
            {
                if (SearchedTaskTo.Position != "CEO" && SearchedTaskTo.Position != "TeamLeader")
                {
                    taskFuns.AddNewItem(new Models.Task()
                    {
                        EndDate = EndDateComplete,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        TaskDiscription = Discription,
                        TaskState = "New",
                        TaskTitle = TaskTitle,
                        TaskType = TaskType,
                        ToUserId = userFuns.FindByUserName(SelectedPerson).id,
                        FromUserId = fromUserId,
                        Argent = argent,
                    });
                    notificationFuns.AddNewItem(new Notifications()
                    {
                        Message = "You Have New Task From " + SearchedUserFrom.UserName,
                        Notif_From = SearchedUserFrom.UserName,
                        Notif_To = SearchedTaskTo.UserName,
                        State = true
                    });
                    _hubContext.Clients.All.SendAsync("AddNotification", "You Have New Task From " + SearchedUserFrom.UserName, SearchedTaskTo.id);
                }
            }
            if (argent == true)
            {
                var SearchedTask = taskFuns.FindTaskByTitle(TaskTitle);
                var UserInprogressTasks = taskFuns.GetInprogressTasksAndNotArgentByTaskTitle(SearchedTaskTo.id);
                foreach (var TaskItem in UserInprogressTasks)
                {
                    if (SearchedTask.EndDate > SearchedTask.StartDate)
                    {
                        var ShiftHours = SearchedTask.EndDate.Hour - SearchedTask.StartDate.Hour;
                        TaskItem.EndDate = TaskItem.EndDate.AddHours(ShiftHours);
                    }
                }
            }

            AccessDB.AccessChanges();
            return RedirectToAction(nameof(LogInPost), new { UserNmae = SearchedUserFrom.UserName, Password = SearchedUserFrom.Password });
        }
        public ActionResult ShowSentTasks(int ClientId)
        {
            GeneralModelView ModellData = new GeneralModelView();
            var UserTasks = taskFuns.FindUserTasksByUserId(ClientId);
            ModellData.User = userFuns.FindByUserId(ClientId);
            ModellData.UserTasks = UserTasks;
            ModellData.TaskComments = new List<Comment>();
            ViewBag.TotalTask = UserTasks.Count();
            ViewBag.TotalInProgressTask = taskFuns.GetAllInProgressTasksPerUserId(ClientId).Count();
            ViewBag.TotaldailyTask = taskFuns.GetAllDailyTasksPerUserId(ClientId).Count();
            ViewBag.TotallongtermTask = taskFuns.GetAlllongtermTasksPerUserId(ClientId).Count();
            ViewBag.TotalsupportiveTask = taskFuns.GetAllsupportiveTasksPerUserId(ClientId).Count();
            ViewBag.TotalClosedTask = taskFuns.GetAllClosedTasksPerUserId(ClientId).Count();
            foreach (var TaskItem in UserTasks)
            {
                ModellData.TaskComments.AddRange(commentFuns.GetAllTaskComments(TaskItem.TaskTitle));
            }
            return View(ModellData);
        }
        public string AddCommentToTask(string CommentContent , string TaskTitle , string UserName)
        {
            commentFuns.AddNewItem(new Comment()
            {
                CommentContent = CommentContent,
                CommentTime = DateTime.Now,
                fromUserUserName = UserName,
                TaskTitle = TaskTitle
            });
            AccessDB.AccessChanges();

            /*********************************** HUB Connection ******************************/
            _hubContext.Clients.All.SendAsync("RecieveCooment", TaskTitle, CommentContent, UserName);
            /*********************************************************************************/
            return "Done";
        }
        public ActionResult ShowTasks(int ClientId, String TaskState)
        {
            User SearchedUser = userFuns.FindByUserId(ClientId);
            List<Task> UserTasks = taskFuns.GetAllUserTasksWith_TaskState_UserId(TaskState, ClientId);
            ViewBag.TaskState = TaskState;
            GeneralModelView ModellData = new GeneralModelView();
            ModellData.User = SearchedUser;
            ModellData.UserTasks = UserTasks;
            ModellData.TaskComments = new List<Comment>();
            foreach (var TaskItem in UserTasks)
            {
                ModellData.TaskComments.AddRange(commentFuns.GetAllTaskComments(TaskItem.TaskTitle));
            }
            return View(ModellData);
        }
        public ActionResult ChangeProcessState(string State, string UserName, string Password, string TaskTitle)
        {
            var SearchedTask = taskFuns.FindTaskByTitle(TaskTitle);
            SearchedTask.TaskState = State;
            if (State == "Closed")
            {
                DateTime DateNow = DateTime.Now;
                DateTime EndDate = SearchedTask.EndDate;
                if (DateNow <= EndDate)
                {
                    SearchedTask.FinalState = "InTime";
                }
                else
                {
                    SearchedTask.FinalState = "Late";
                }
                notificationFuns.AddNewItem(new Notifications()
                {
                    Message = UserName + " Closed Task " + TaskTitle + " Now",
                    Notif_From = UserName,
                    Notif_To = userFuns.FindByUserId(SearchedTask.FromUserId).UserName,
                    State = true
                });
                _hubContext.Clients.All.SendAsync("AddNotification", UserName + " Closed Task " + TaskTitle + " Now", userFuns.FindByUserId(SearchedTask.FromUserId).UserName);

            }
            taskFuns.EditItem(SearchedTask.id, SearchedTask);
            AccessDB.AccessChanges();
            return RedirectToAction(nameof(LogInPost), new { UserNmae = UserName, Password = Password });
        }
        public ActionResult SignOut(int UserId)
        {
            var SearchedUser = userFuns.FindByUserId(UserId);
            attendanceFuns.LogOutAttendance(SearchedUser.UserName);
            AccessDB.AccessChanges();
            return RedirectToAction(nameof(Index));
        }
        public string ChangePassword(string currentPassword, string NewPassword, string ConfirmNewPassword, int UserId)
        {
            var searchedUser = userFuns.FindByUserId(UserId);
            if (searchedUser.Password == currentPassword && NewPassword == ConfirmNewPassword)
            {
                return "Done";
            }
            return "Failed";
        }
        public string ConfirmCode(string ConfirmInput, string ConfirmCode, int UserId, string NewPassWord)
        {
            if (ConfirmInput.Trim() == ConfirmCode.Trim())
            {
                var searchedUser = userFuns.FindByUserId(UserId);
                searchedUser.Password = NewPassWord;
                userFuns.EditItem(UserId, searchedUser);
                AccessDB.AccessChanges();
                return "Done";
            }
            return "Failed";
        }
        public string DeleteNotification(int NotificationId)
        {
            if (NotificationId == 0)
            {
                return "Failed";
            }
            notificationFuns.DeleteItem(NotificationId);
            AccessDB.AccessChanges();
            return "Done";
        }
    }
}
