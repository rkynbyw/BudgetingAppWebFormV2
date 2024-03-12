using BudgetingApp.BO;
using System.Collections.Generic;

namespace BudgetingApp.DAL.Interfaces
{
    public interface ITransactionCategoryDAL : Icrud<TransactionCategory>
    {
        IEnumerable<TransactionCategory> GetCategoryNameByType(int transactionTypeId);
    }
}
