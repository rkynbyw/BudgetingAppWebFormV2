using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using BudgetingApp.BO;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BudgetingApp.BLL
{
    public class TransactionBLL : ITransactionBLL
    {

        private readonly ITransactionDAL _transactionDAL;
        public TransactionBLL()
        {
            _transactionDAL = new TransactionDAL();
        }

        public IEnumerable<TransactionDTO> GetUserTransactionV2(int userID, int? year = null, int? month = null, int? transactionCategoryID = null, int? transactionTypeID = null)
        {
            var transactions = _transactionDAL.GetUserTransactionV2(userID, year, month, transactionCategoryID, transactionTypeID);
            var transactionDTOs = new List<TransactionDTO>();

            foreach (var transaction in transactions)
            {
                var transactionDTO = new TransactionDTO
                {
                    TransactionID = transaction.TransactionID,
                    UserID = transaction.UserID,
                    WalletID = transaction.WalletID,
                    TransactionCategoryID = transaction.TransactionCategoryID,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    Description = transaction.Description,
                    WalletName = transaction.Wallet.WalletType.Name,
                    Wallet = new WalletDTO
                    {
                        WalletType = new WalletTypeDTO
                        {
                            Name = transaction.Wallet.WalletType.Name
                        }
                    },
                    TransactionCategory = new TransactionCategoryDTO
                    {
                        Name = transaction.TransactionCategory.Name,
                        TransactionTypeID = transaction.TransactionCategory.TransactionTypeID
                    }
                };
                transactionDTOs.Add(transactionDTO);
            }

            return transactionDTOs;
        }

        public IEnumerable<TransactionDTO> GetAll()
        {
            List<TransactionDTO> listTransactionsDto = new List<TransactionDTO>();
            var transactions = _transactionDAL.GetAll();
            foreach (var transaction in transactions)
            {
                listTransactionsDto.Add(new TransactionDTO
                {
                    TransactionID = transaction.TransactionID,
                    TransactionCategoryID = transaction.TransactionCategoryID,
                    UserID = transaction.UserID,
                    WalletID = transaction.WalletID,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    Description = transaction.Description
                });
            }
            return listTransactionsDto;
        }



        public IEnumerable<TransactionDTO> GetUserTransaction(int UserID)
        {
            var userIDdebug = UserID;
            List<TransactionDTO> transactionDTOs = new List<TransactionDTO>();
            var transactions = _transactionDAL.GetUserTransaction(UserID);
            foreach (var transaction in transactions)
            {
                transactionDTOs.Add(new TransactionDTO
                {
                    TransactionID = transaction.TransactionID,
                    UserID = transaction.UserID,
                    WalletID = transaction.WalletID,
                    TransactionCategoryID = transaction.TransactionCategoryID,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    Description = transaction.Description,
                    WalletName = transaction.Wallet.WalletType.Name,
                    Wallet = new WalletDTO
                    {
                        WalletType = new WalletTypeDTO
                        {
                            Name = transaction.Wallet.WalletType.Name
                        }
                    },

                    TransactionCategory = new TransactionCategoryDTO
                    {
                        Name = transaction.TransactionCategory.Name,
                        TransactionTypeID = transaction.TransactionCategory.TransactionTypeID,

                    }
                });
            }
            return transactionDTOs;


            //List<WalletDTO> walletsbytypeDTO = new List<WalletDTO>();
            //var wallets = _walletDAL.GetWalletByType(WalletTypeID);
            //foreach (var wallet in wallets)
            //{
            //    walletsbytypeDTO.Add(new WalletDTO
            //    {
            //        WalletID = wallet.WalletID,
            //        WalletTypeID = wallet.WalletTypeID,
            //        Balance = wallet.Balance,
            //        UserID = wallet.UserID,
            //        WalletType = new WalletTypeDTO
            //        {
            //            WalletTypeID = wallet.WalletType.WalletTypeID,
            //            Name = wallet.WalletType.Name
            //        }
            //    });
            //}
            //return walletsbytypeDTO;
        }

        public void Insert(TransactionDTO entity)
        {
            if (entity.Date == null || entity.Date == DateTime.MinValue)
            {
                throw new ArgumentNullException("Date is required");
            }

            // Validasi untuk Amount (Decimal)
            if (entity.Amount <= 0)
            {
                throw new ArgumentNullException("Amount must be greater than zero");
            }

            try
            {
                var newTransaction = new Transaction
                {

                    Date = entity.Date,
                    TransactionCategoryID = entity.TransactionCategoryID,
                    Amount = entity.Amount,
                    WalletID = entity.WalletID,
                    Description = entity.Description,
                    UserID = entity.UserID,

                };
                _transactionDAL.Insert(newTransaction);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public void Update(TransactionDTO entity)
        {
            try
            {
                var newTransaction = new Transaction
                {
                    TransactionID = entity.TransactionID,
                    Date = entity.Date,
                    TransactionCategoryID = entity.TransactionCategoryID,
                    Amount = entity.Amount,
                    WalletID = entity.WalletID,
                    Description = entity.Description,
                    UserID = entity.UserID

                };
                _transactionDAL.Update(newTransaction);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public TransactionDTO GetById(int id)
        {
            TransactionDTO transactionDto = new TransactionDTO();
            var transaction = _transactionDAL.GetById(id);
            if (transaction != null)
            {
                transactionDto.TransactionID = transaction.TransactionID;
                transactionDto.UserID = transaction.UserID;
                transactionDto.WalletID = transaction.WalletID;
                transactionDto.TransactionCategoryID = transaction.TransactionCategoryID;
                transactionDto.Amount = transaction.Amount;
                transactionDto.Date = transaction.Date;
                transactionDto.Description = transaction.Description;
                //transactionDto.WalletName = transaction.Wallet.WalletType.Name,
            }
            else
            {
                throw new ArgumentException($"Transaction {id} not found");
            }
            return transactionDto;


            //CategoryDTO categoryDto = new CategoryDTO();
            //var category = _categoryDAL.GetById(id);
            //if (category != null)
            //{
            //    categoryDto.CategoryID = category.CategoryID;
            //    categoryDto.CategoryName = category.CategoryName;
            //}
            //else
            //{
            //    throw new ArgumentException($"Category {id} not found");
            //}
            //return categoryDto;
        }

        public decimal GetUserExpense(int userID)
        {
            try
            {
                return _transactionDAL.GetUserExpense(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal GetUserExpenseByMonth(int userID, int year, int month, int transactionCategoryID)
        {
            try
            {
                return _transactionDAL.GetUserExpenseByMonth(userID, year, month, transactionCategoryID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public decimal GetUserIncome(int userID)
        {
            try
            {
                return _transactionDAL.GetUserIncome(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int transactionID)
        {
            if (transactionID <= 0)
            {
                throw new ArgumentException("TransactionID is required");
            }
            try
            {
                _transactionDAL.Delete(transactionID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
