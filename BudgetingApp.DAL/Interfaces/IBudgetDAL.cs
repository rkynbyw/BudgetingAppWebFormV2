using BudgetingApp.BO;
using System.Collections.Generic;

namespace BudgetingApp.DAL.Interfaces
{
    public interface IBudgetDAL : Icrud<Budget>
    {
        IEnumerable<Budget> GetUserBudgets(int userId);
    }
}
