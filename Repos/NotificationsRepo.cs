using Elite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Repos
{
    public class NotificationsRepo : GeneralInterface<Notifications>
    {
        private DBCoClass MyDb { get; set; }
        public NotificationsRepo(DBCoClass MyDb)
        {
            this.MyDb = MyDb;
        }
        public void AddNewItem(Notifications NewItem)
        {
            MyDb.Notification.Add(NewItem);
        }

        public void DeleteItem(int id)
        {
            var SearchedNotification = MyDb.Notification.Where(x => x.id == id).FirstOrDefault();
            MyDb.Notification.Remove(SearchedNotification);
        }

        public void EditItem(int id, Notifications UpdateData)
        {
            var SearchedNotification = MyDb.Notification.Where(x => x.id == id).FirstOrDefault();
            MyDb.Notification.Update(SearchedNotification);
        }

        public List<Notifications> GetAllData()
        {
            return MyDb.Notification.ToList();
        }
        public List<Notifications> GetAllMyNotifications(string UserName)
        {
            return MyDb.Notification.ToList().Where(x => x.Notif_To == UserName).ToList();
        }
    }
}
