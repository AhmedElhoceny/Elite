using Elite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = Elite.Models.Task;

namespace Elite.Repos
{
    public class TaskRepo : GeneralInterface<Task>
    {
        public DBCoClass MyDb { get; set; }
        public TaskRepo(DBCoClass MyDb)
        {
            this.MyDb = MyDb;
        }
        public void AddNewItem(Task NewItem)
        {
            MyDb.Tasks.Add(NewItem);
        }

        public void DeleteItem(int id)
        {
            var SearchedTask = MyDb.Tasks.ToList().Where(x => x.id == id).FirstOrDefault();
            MyDb.Tasks.Remove(SearchedTask);
        }

        public void EditItem(int id, Task UpdateData)
        {
            var SearchedTask = MyDb.Tasks.ToList().Where(x => x.id == id).FirstOrDefault();
            MyDb.Tasks.Update(UpdateData);
        }

        public List<Task> GetAllData()
        {
            return MyDb.Tasks.ToList();
        }
        public List<Task> GetAllUserTasks(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.ToUserId == UserId).ToList();
        }
        public List<Task> GetAllInProgressTasksPerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.ToUserId == UserId && x.TaskState == "InProgress").ToList();
        }
        public List<Task> GetAllClosedTasksPerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.ToUserId == UserId && x.TaskState == "Closed").ToList();
        }
        public List<Task> GetAllDailyTasksPerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.ToUserId == UserId && x.TaskType == "daily").ToList();
        }
        public List<Task> GetAlllongtermTasksPerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.ToUserId == UserId && x.TaskType == "longterm").ToList();
        }
        public List<Task> GetAllsupportiveTasksPerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.ToUserId == UserId && x.TaskType == "supportive").ToList();
        }
        public List<Task> GetAllDailyDoneTasksPerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.TaskType == "daily" && x.ToUserId == UserId && x.FinalState == "InTime" && x.TaskState == "Closed").ToList();
        }
        public List<Task> GetAllLongTemsDoneTasksPerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.TaskType == "longterm" && x.ToUserId == UserId && x.FinalState == "InTime" && x.TaskState == "Closed").ToList();
        }
        public List<Task> GetAllSupportiveTasksDonePerUserId(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.TaskType == "supportive" && x.ToUserId == UserId && x.TaskState == "Closed").ToList();
        }
        public Task FindTaskByTitle(string TaskTite)
        {
            return MyDb.Tasks.ToList().Where(x => x.TaskTitle == TaskTite).FirstOrDefault();
        }
        public List<Task> GetInprogressTasksAndNotArgentByTaskTitle(int UserId)
        {
            return MyDb.Tasks.ToList().Where(x => x.ToUserId == UserId && x.TaskState == "InProgress" && x.Argent == false).ToList();
        }
        public List<Task> FindUserTasksByUserId(int ClientId)
        {
            return MyDb.Tasks.ToList().Where(x => x.FromUserId == ClientId).ToList();
        }
        public List<Task> GetAllUserTasksWith_TaskState_UserId(string TaskState , int ClientId)
        {
            return MyDb.Tasks.ToList().Where(x => x.TaskState == TaskState && x.ToUserId == ClientId).ToList();
        }
    }
}
