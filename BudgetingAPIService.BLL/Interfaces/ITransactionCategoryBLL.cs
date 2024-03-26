using BudgetingAPIService.BLL.DTOs;

namespace BudgetingAPIService.BLL.Interfaces
{
    public interface ITransactionCategoryBLL
    {
        IEnumerable<TransactionCategoryDTO> GetCategoryNameByType(int transactionTypeId);
    }
}
