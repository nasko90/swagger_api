using System.Collections.Generic;
using Data_Access.Models;

namespace Data_Access.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        bool Add(T item);
        bool Remove(int id);
        bool Update(T item);
        T FindBy<TU>(string colName, TU value);
        IEnumerable<T> FindAll();
    }
}