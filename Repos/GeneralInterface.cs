using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elite.Repos
{
    interface GeneralInterface<T>
    {
        List<T> GetAllData();
        void AddNewItem(T NewItem);
        void DeleteItem(int id);
        void EditItem(int id, T UpdateData);
    }
}
