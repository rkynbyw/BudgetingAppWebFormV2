using BudgetingApp.BLL.DTOs;
using System.Collections.Generic;

namespace BudgetingApp.BLL.Interfaces
{
    public interface ITransactionBLL
    {
        IEnumerable<TransactionDTO> GetAll();
        IEnumerable<TransactionDTO> GetUserTransaction(int UserID);

        void Insert(TransactionDTO entity);
        void Update(TransactionDTO entity);
        void Delete(int transactionID);
        TransactionDTO GetById(int id);

        decimal GetUserExpense(int userID);

        decimal GetUserIncome(int userID);

    }
}
