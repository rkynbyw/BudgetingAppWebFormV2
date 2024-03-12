using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;
using System.Collections.Generic;

namespace BudgetingApp.BLL
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

//List<TransactionCategoryDTO> transactionCategoryDTOs = new List<TransactionCategoryDTO>();
//var categories = _categoryDAL.GetCategoryNameByType(transactionTypeId);
//foreach (var category in categories)
//{
//    transactionCategoryDTOs.Add(new TransactionCategoryDTO
//    {
//        TransactionCategoryID = category.TransactionCategoryID,
//        TransactionTypeID = category.TransactionTypeID,
//        Name = category.Name,

//    });
//}
//return transactionCategoryDTOs;

//IEnumerable<WalletDTO> IWalletBLL.GetWalletByType(int WalletTypeID)
//        {
//    List<WalletDTO> walletsbytypeDTO = new List<WalletDTO>();
//    var wallets = _walletDAL.GetWalletByType(WalletTypeID);
//    foreach (var wallet in wallets)
//    {
//        walletsbytypeDTO.Add(new WalletDTO
//        {
//            WalletID = wallet.WalletID,
//            WalletTypeID = wallet.WalletTypeID,
//            Balance = wallet.Balance,
//            UserID = wallet.UserID,
//            WalletType = new WalletTypeDTO
//            {
//                WalletTypeID = wallet.WalletType.WalletTypeID,
//                Name = wallet.WalletType.Name
//            }
//        });
//    }
//    return walletsbytypeDTO;


