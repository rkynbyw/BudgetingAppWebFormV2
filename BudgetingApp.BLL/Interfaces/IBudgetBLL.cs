using BudgetingApp.BLL.DTOs;
using System.Collections.Generic;

namespace BudgetingApp.BLL.Interfaces
{
    public interface IBudgetBLL
    {
        void InsertBudget(BudgetDTO entity);
        void UpdateBudget(BudgetDTO entity);
        void DeleteBudget(int budgetID);
        BudgetDTO GetBudgetByID(int budgetID);
        IEnumerable<BudgetDTO> GetUserBudgets(int userID);


    }
}
