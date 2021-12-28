using Elite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Repos
{
    public class CommentRepo : GeneralInterface<Comment>
    {
        public DBCoClass MyDb { get; set; }
        public CommentRepo(DBCoClass MyDb)
        {
            this.MyDb = MyDb;
        }

        public void AddNewItem(Comment NewItem)
        {
            MyDb.Comments.Add(NewItem);
        }

        public void DeleteItem(int id)
        {
            var SearchedComment = MyDb.Comments.ToList().Where(x => x.id == id).FirstOrDefault();
            MyDb.Comments.Remove(SearchedComment);
        }

        public void EditItem(int id, Comment UpdateData)
        {
            var SearchedComment = MyDb.Comments.ToList().Where(x => x.id == id).FirstOrDefault();
            MyDb.Comments.Update(SearchedComment);
        }

        public List<Comment> GetAllData()
        {
            return MyDb.Comments.ToList();
        }
        public List<Comment> GetAllTaskComments(string TaskTitle)
        {
            return MyDb.Comments.ToList().Where(x => x.TaskTitle == TaskTitle).ToList();
        }
    }
}
