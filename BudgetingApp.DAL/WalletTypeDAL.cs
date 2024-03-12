using BudgetingApp.BO;
using BudgetingApp.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BudgetingApp.DAL
{
    public class WalletTypeDAL : IWalletTypeDAL
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"].ConnectionString;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WalletType> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"GetWalletType";

                var result = conn.Query<WalletType>(strSql, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }

        public WalletType GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(WalletType entity)
        {
            throw new NotImplementedException();
        }

        public void Update(WalletType entity)
        {
            throw new NotImplementedException();
        }
    }
}
