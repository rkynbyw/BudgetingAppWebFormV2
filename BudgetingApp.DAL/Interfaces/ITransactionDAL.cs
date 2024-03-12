

using BudgetingApp.BO;
using System.Collections.Generic;

namespace BudgetingApp.DAL.Interfaces
{
    public interface ITransactionDAL : Icrud<Transaction>
    {
        IEnumerable<Transaction> GetUserTransaction(int UserID);

        decimal GetUserExpense(int userID);

        decimal GetUserIncome(int userID);


    }
}
