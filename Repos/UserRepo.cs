using Elite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Repos
{
    public class UserRepo : GeneralInterface<User>
    {
        public DBCoClass MyDb { get; set; }
        public UserRepo(DBCoClass MyDb)
        {
            this.MyDb = MyDb;
        }

        public void AddNewItem(User NewItem)
        {
            var OurUsers = GetAllData();
            if (OurUsers.Where(x => x.UserName == NewItem.UserName || x.Email == NewItem.Email ).FirstOrDefault() == null)
            {
                MyDb.Users.Add(NewItem);
            }
        }

        public void DeleteItem(int id)
        {
            var SearchedUser = MyDb.Users.ToList().Where(x => x.id == id).FirstOrDefault();
            if (SearchedUser != null)
            {
                MyDb.Users.ToList().Remove(SearchedUser);
            }
        }

        public void EditItem(int id, User UpdateData)
        {
            var SearchedUser = MyDb.Users.ToList().Where(x => x.id == id).FirstOrDefault();
            if (SearchedUser != null)
            {
                MyDb.Users.Update(UpdateData);
            }
        }

        public List<User> GetAllData()
        {
            return MyDb.Users.ToList();
        }
        public User FindByUserName(string UserName)
        {
            return MyDb.Users.Where(x => x.UserName == UserName).FirstOrDefault();
        }
        public User FindByUserId(int Id)
        {
            return MyDb.Users.Where(x => x.id == Id).FirstOrDefault();
        }
        public string SearchUser(string UserName , string PassWord)
        {
            var OurUsers = GetAllData();
            if (OurUsers.Where(x => x.UserName == UserName && x.Password == PassWord).FirstOrDefault() != null)
            {
                return "Yes";
            }
            return "No";
        }
    }
}
