using System.Collections.Generic;

namespace BudgetingApp.DAL.Interfaces
{
    public interface Icrud<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
