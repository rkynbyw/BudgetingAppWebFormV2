using BudgetingAPIService.BLL.DTOs;

namespace BudgetingAPIService.BLL.Interfaces
{
    public interface IBudgetBLL
    {
        void InsertBudget(BudgetCreateDTO entity);
        void UpdateBudget(BudgetUpdateDTO entity);
        void DeleteBudget(int budgetID);
        BudgetDTO GetBudgetByID(int budgetID);
        IEnumerable<BudgetDTO> GetUserBudgets(int userID);
    }
}
