﻿using BudgetingApp.BO;
using BudgetingApp.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Transaction = BudgetingApp.BO.Transaction;

namespace BudgetingApp.DAL
{
    public class TransactionDAL : ITransactionDAL
    {

        private string GetConnectionString()
        {
            //return ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"].ConnectionString;
            return Helper.GetConnectionString();
        }

        public IEnumerable<Transaction> GetUserTransactionV2(int userID, int? year = null, int? month = null, int? transactionCategoryID = null, int? transactionTypeID = null)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                List<Transaction> transactions = new List<Transaction>();
                var strSql = "GetUserTransactionV2"; // Ubah menjadi nama stored procedure Anda
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Year", year != null ? (object)year : DBNull.Value);
                cmd.Parameters.AddWithValue("@Month", month != null ? (object)month : DBNull.Value);
                cmd.Parameters.AddWithValue("@TransactionCategoryID", transactionCategoryID != null ? (object)transactionCategoryID : DBNull.Value);
                cmd.Parameters.AddWithValue("@TransactionTypeID", transactionTypeID != null ? (object)transactionTypeID : DBNull.Value);


                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var transaction = new Transaction()
                    {
                        TransactionID = Convert.IsDBNull(dr["TransactionID"]) ? 0 : Convert.ToInt32(dr["TransactionID"]),
                        UserID = Convert.IsDBNull(dr["UserID"]) ? 0 : Convert.ToInt32(dr["UserID"]),
                        WalletID = Convert.IsDBNull(dr["WalletID"]) ? 0 : Convert.ToInt32(dr["WalletID"]),
                        TransactionCategoryID = Convert.IsDBNull(dr["TransactionCategoryID"]) ? 0 : Convert.ToInt32(dr["TransactionCategoryID"]),
                        Amount = Convert.IsDBNull(dr["Amount"]) ? 0 : Convert.ToDecimal(dr["Amount"]),
                        Date = Convert.IsDBNull(dr["Date"]) ? DateTime.MinValue : Convert.ToDateTime(dr["Date"]),
                        Description = Convert.IsDBNull(dr["Description"]) ? string.Empty : dr["Description"].ToString(),
                        Wallet = new Wallet()
                        {
                            WalletType = new WalletType()
                            {
                                Name = Convert.IsDBNull(dr["WalletName"]) ? string.Empty : dr["WalletName"].ToString(),
                            }
                        },
                        TransactionCategory = new TransactionCategory()
                        {
                            Name = Convert.IsDBNull(dr["TransactionCategory"]) ? string.Empty : dr["TransactionCategory"].ToString(),
                            TransactionTypeID = Convert.IsDBNull(dr["TransactionTypeID"]) ? 0 : Convert.ToInt32(dr["TransactionTypeID"])
                        }
                    };
                    transactions.Add(transaction);
                }

                return transactions;

            }
        }


        public IEnumerable<Transaction> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"GetAllTransaction";

                var result = conn.Query<Transaction>(strSql, commandType: System.Data.CommandType.StoredProcedure);

                return result;
            }
        }


        public Transaction GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "SELECT * FROM Transactions WHERE TransactionID = @TransactionID";
                var param = new { TransactionID = id };
                var result = conn.QuerySingleOrDefault<Transaction>(strSql, param);
                return result;
            }
        }

        public void Insert(Transaction entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "[usp_CreateTransactions]";
                var param = new
                {
                    UserID = entity.UserID,
                    TransactionCategoryID = entity.TransactionCategoryID,
                    Amount = entity.Amount,
                    WalletID = entity.WalletID,
                    Date = entity.Date,
                    Description = entity.Description,
                };
                try
                {
                    int result = conn.Execute(strSql, param, commandType: CommandType.StoredProcedure);
                    if (result != 2)
                    {
                        throw new Exception("Transaksi tidak berhasil ditambahkan");
                    }

                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException?.Message} - {sqlEx.Number}");

                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Kesalahan: " + ex.Message);
                }
            }
        }



        public void Update(Transaction entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "usp_EditTransactionsRev";
                var parameters = new
                {
                    TransactionID = entity.TransactionID,
                    UserID = entity.UserID,
                    NewCategoryID = entity.TransactionCategoryID,
                    NewAmount = entity.Amount,
                    NewWalletID = entity.WalletID,
                    NewDate = entity.Date,
                    NewDescription = entity.Description
                };

                try
                {
                    int result = conn.Execute(strSql, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (result != 4)
                    {
                        throw new Exception("Transaksi berhasil dilakukan update");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable<Transaction> GetUserTransaction(int UserID)
        {

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                List<Transaction> transactions = new List<Transaction>();
                var strSql = @"GetUserTransaction";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var transaction = new Transaction()
                        {
                            //TransactionID = Convert.ToInt32(dr["TransactionID"]),
                            TransactionID = dr["TransactionID"] != DBNull.Value ? Convert.ToInt32(dr["TransactionID"]) : 0,
                            UserID = Convert.ToInt32(dr["UserID"]),
                            WalletID = Convert.ToInt32(dr["WalletID"]),
                            TransactionCategoryID = Convert.ToInt32(dr["TransactionCategoryID"]),
                            Amount = Convert.ToDecimal(dr["Amount"]),
                            Date = dr["Date"] != DBNull.Value ? Convert.ToDateTime(dr["Date"]) : DateTime.MinValue,
                            Description = dr["Description"] != DBNull.Value ? dr["Description"].ToString() : string.Empty,


                            Wallet = new Wallet()
                            {
                                WalletType = new WalletType()
                                {
                                    Name = dr["WalletName"].ToString(),
                                }
                            },
                            TransactionCategory = new TransactionCategory()
                            {
                                Name = dr["TransactionCategory"].ToString(),
                                TransactionTypeID = Convert.ToInt32(dr["TransactionTypeID"])
                            }
                        };
                        transactions.Add(transaction);
                    }
                }
                return transactions;
            }
        }

        public decimal GetUserExpense(int userID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "GetUserExpense";
                var param = new { UserID = userID };

                try
                {
                    // Execute the stored procedure and retrieve the result
                    decimal result = conn.QuerySingleOrDefault<decimal>(strSql, param, commandType: CommandType.StoredProcedure);

                    // Return the calculated total expense
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exception (e.g., log the error, throw exception, etc.)
                    throw new ArgumentException($"{sqlEx.InnerException?.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new ArgumentException("Error: " + ex.Message);
                }
            }
        }

        public decimal GetUserExpenseByMonth(int userID, int year, int month, int transactionCategoryID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "GetUserExpenseByCategoryAndMonth";
                var parameters = new
                {
                    UserID = userID,
                    Year = year,
                    Month = month,
                    TransactionCategoryID = transactionCategoryID
                };

                try
                {
                    decimal result = conn.QuerySingleOrDefault<decimal>(strSql, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exception
                    throw new ArgumentException($"{sqlEx.InnerException?.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new ArgumentException("Error: " + ex.Message);
                }
            }
        }

        public decimal GetUserIncome(int userID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "GetUserIncome";
                var param = new { UserID = userID };

                try
                {

                    decimal result = conn.QuerySingleOrDefault<decimal>(strSql, param, commandType: CommandType.StoredProcedure);

                    return result;
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException?.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {

                    throw new ArgumentException("Error: " + ex.Message);
                }
            }
        }

        public void Delete(int transactionID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "DeleteUserTransaction";
                var parameters = new
                {
                    TransactionID = transactionID,
                };

                try
                {
                    int result = conn.Execute(strSql, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (result != 2)
                    {
                        throw new Exception("Transaksi berhasil dihapus");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

}

