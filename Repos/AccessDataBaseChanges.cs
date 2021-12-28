using Elite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Repos
{
    public class AccessDataBaseChanges
    {
        private DBCoClass MyDb { get; set; }
        public AccessDataBaseChanges(DBCoClass MyDb)
        {
            this.MyDb = MyDb;
        }

        public void AccessChanges()
        {
            MyDb.SaveChanges();
        }
    }
}
