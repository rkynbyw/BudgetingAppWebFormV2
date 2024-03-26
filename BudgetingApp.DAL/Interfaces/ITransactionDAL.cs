using BudgetingApp.BO;
using System.Collections.Generic;

namespace BudgetingApp.DAL.Interfaces
{
    public interface ITransactionDAL : Icrud<Transaction>
    {
        IEnumerable<Transaction> GetUserTransaction(int UserID);

        decimal GetUserExpense(int userID);

        decimal GetUserIncome(int userID);

        IEnumerable<Transaction> GetUserTransactionV2(int userID, int? year = null, int? month = null, int? transactionCategoryID = null, int? transactionTypeID = null);

        decimal GetUserExpenseByMonth(int userID, int year, int month, int transactionCategoryID);

    }
}
