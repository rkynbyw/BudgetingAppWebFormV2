using BudgetingApp.BO;
using BudgetingApp.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BudgetingApp.DAL
{
    public class BudgetDAL : IBudgetDAL
    {
        private string GetConnectionString()
        {
            // Mengambil koneksi dari konfigurasi aplikasi
            // return ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"].ConnectionString;
            return Helper.GetConnectionString();
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "DeleteBudget";
                var parameters = new { BudgetID = id };
                try
                {
                    int result = conn.Execute(strSql, parameters, commandType: CommandType.StoredProcedure);
                    if (result != 1)
                    {
                        throw new Exception("Delete operation failed.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log the error, throw a custom exception, etc.)
                    throw new Exception("Error deleting budget: " + ex.Message);
                }
            }
        }

        public IEnumerable<Budget> GetAll()
        {
            throw new NotImplementedException();
        }

        public Budget GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "GetBudgetById";
                var parameters = new { BudgetID = id };
                var result = conn.Query<Budget, TransactionCategory, Budget>(
                    strSql,
                    (budget, transactionCategory) =>
                    {
                        budget.TransactionCategory = transactionCategory;
                        return budget;
                    },
                    parameters,
                    splitOn: "TransactionCategoryID",
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Budget> GetUserBudgets(int userId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "GetUserBudget";
                var parameters = new { UserID = userId };
                var result = conn.Query<Budget, TransactionCategory, Budget>(
                    strSql,
                    (budget, transactionCategory) =>
                    {
                        budget.TransactionCategory = transactionCategory;
                        return budget;
                    },
                    parameters,
                    splitOn: "TransactionCategoryID",
                    commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }


        public void Insert(Budget entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "InsertBudget";
                var parameters = new
                {
                    MonthDate = entity.MonthDate,
                    Amount = entity.Amount,
                    UserID = entity.UserID,
                    TransactionCategoryID = entity.TransactionCategoryID
                };
                try
                {
                    int result = conn.Execute(strSql, parameters, commandType: CommandType.StoredProcedure);
                    if (result != 1)
                    {
                        throw new Exception("Insert operation failed.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log the error, throw a custom exception, etc.)
                    throw new Exception("Error inserting budget: " + ex.Message);
                }
            }
        }

        public void Update(Budget entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "UpdateBudget";
                var parameters = new
                {
                    BudgetID = entity.BudgetID,
                    MonthDate = entity.MonthDate,
                    Amount = entity.Amount,
                    TransactionCategoryID = entity.TransactionCategoryID
                };
                try
                {
                    int result = conn.Execute(strSql, parameters, commandType: CommandType.StoredProcedure);
                    if (result != 1)
                    {
                        throw new Exception("Update operation failed.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log the error, throw a custom exception, etc.)
                    throw new Exception("Error updating budget: " + ex.Message);
                }
            }
        }
    }
}
