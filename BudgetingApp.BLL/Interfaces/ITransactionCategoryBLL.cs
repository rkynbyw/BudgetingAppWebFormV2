using BudgetingApp.BLL.DTOs;
using System.Collections.Generic;

namespace BudgetingApp.BLL.Interfaces
{
    public interface ITransactionCategoryBLL
    {
        IEnumerable<TransactionCategoryDTO> GetCategoryNameByType(int transactionTypeId);
    }
}
