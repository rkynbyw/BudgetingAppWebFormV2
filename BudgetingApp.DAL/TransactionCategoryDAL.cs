using BudgetingApp.BO;
using BudgetingApp.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BudgetingApp.DAL
{
    public class TransactionCategoryDAL : ITransactionCategoryDAL
    {

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"].ConnectionString;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionCategory> GetAll()
        {
            throw new NotImplementedException();
        }

        public TransactionCategory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TransactionCategory entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TransactionCategory entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionCategory> GetCategoryNameByType(int transactionTypeId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"GetCategoryNameByType";

                var parameters = new DynamicParameters();
                parameters.Add("@TransactionTypeID", transactionTypeId);

                var result = conn.Query<TransactionCategory>(strSql, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
