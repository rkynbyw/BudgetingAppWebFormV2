using BudgetingAPIService.BLL.DTOs;
using BudgetingAPIService.BLL.Interfaces;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;

namespace BudgetingAPIService.BLL
{
    public class TransactionCategoryBLL : ITransactionCategoryBLL
    {
        private readonly ITransactionCategoryDAL _categoryDAL;

        public TransactionCategoryBLL()
        {
            _categoryDAL = new TransactionCategoryDAL();
        }

        public IEnumerable<TransactionCategoryDTO> GetCategoryNameByType(int transactionTypeId)
        {
            List<TransactionCategoryDTO> transactionCategoryDTOs = new List<TransactionCategoryDTO>();
            var categories = _categoryDAL.GetCategoryNameByType(transactionTypeId);
            foreach (var category in categories)
            {
                transactionCategoryDTOs.Add(new TransactionCategoryDTO
                {
                    TransactionCategoryID = category.TransactionCategoryID,
                    TransactionTypeID = category.TransactionTypeID,
                    Name = category.Name,

                });
            }
            return transactionCategoryDTOs;
        }
    }
}
