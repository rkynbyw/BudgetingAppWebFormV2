using BudgetingAPIService.BLL.DTOs;

namespace BudgetingAPIService.BLL.Interfaces
{
    public interface ITransactionBLL
    {
        IEnumerable<TransactionDTO> GetAll();
        IEnumerable<TransactionDTO> GetUserTransaction(int UserID);
        void Insert(TransactionCreateDTO entity);
        void Update(TransactionUpdateDTO entity);
        void Delete(int transactionID);
        TransactionDTO GetById(int id);
        decimal GetUserExpense(int userID);
        decimal GetUserIncome(int userID);
        IEnumerable<TransactionDTO> GetUserTransactionV2(int userID, int? year = null, int? month = null, int? transactionCategoryID = null, int? transactionTypeID = null);
        decimal GetUserExpenseByMonth(int userID, int transactionCategoryID, int year, int month);
    }
}
