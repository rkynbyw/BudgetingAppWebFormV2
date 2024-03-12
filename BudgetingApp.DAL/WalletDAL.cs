using BudgetingApp.BO;
using BudgetingApp.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BudgetingApp.DAL
{
    public class WalletDAL : IWalletDAL
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"].ConnectionString;
        }

        public IEnumerable<Wallet> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"GetBalance";

                var result = conn.Query<Wallet>(strSql, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }



        public Wallet GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "SELECT w.WalletID, w.WalletTypeID, w.Balance, w.UserID, wt.WalletTypeID, wt.Name FROM Wallet w JOIN WalletType wt ON w.WalletTypeID = wt.WalletTypeID WHERE WalletID = @WalletID";
                var param = new { WalletID = id };

                Console.WriteLine($"SQL Query: {strSql}");
                Console.WriteLine($"Parameters: {param}");

                // Execute the query and split the result by WalletTypeID using Dapper's MultiMap feature
                var result = conn.Query<Wallet, WalletType, Wallet>(
                    strSql,
                    (wallet, walletType) =>
                    {
                        wallet.WalletType = walletType; // Assuming you have a WalletType property in your Wallet class
                        return wallet;
                    },
                    param,
                    splitOn: "WalletTypeID"
                ).SingleOrDefault();

                return result;
            }
        }

        public IEnumerable<Wallet> GetByUser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> GetByWallet()
        {
            throw new NotImplementedException();
        }



        public IEnumerable<Wallet> GetWalletByType(int WalletTypeID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"GetWalletByType";

                var parameters = new DynamicParameters();
                parameters.Add("@WalletTypeID", WalletTypeID);

                var result = conn.Query<Wallet>(strSql, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }

        public void TransferWallet(int WalletSCID, int WalletDSTID, int Amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> GetWalletDataByUser(int UserID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {

                //List<Wallet> wallets = new List<Wallet>();
                //var strSql = @"Select * from Wallet w join WalletType wt on w.WalletTypeID = wt.WalletTypeID where w.UserID = @UserID";

                //var parameters = new DynamicParameters();
                //parameters.Add("@UserID", UserID);

                //var result = conn.Query<Wallet>(strSql, parameters);
                //return result;

                var strSql = @"
                SELECT 
                    w.WalletID, 
                    w.WalletTypeID, 
                    w.Balance, 
                    w.UserID, 
                    wt.WalletTypeID, 
                    wt.Name
                FROM Wallet w
                JOIN WalletType wt ON w.WalletTypeID = wt.WalletTypeID
                WHERE w.UserID = @UserID";

                var parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);

                var result = conn.Query<Wallet, WalletType, Wallet>(
                    strSql,
                    (wallet, walletType) =>
                    {
                        wallet.WalletType = walletType;
                        return wallet;
                    },
                    parameters,
                    splitOn: "WalletTypeID"
                );

                return result;




            }
        }

        public void Insert(Wallet entity)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                string sql = "usp_CreateWallet";
                var param = new
                {
                    UserID = entity.UserID,
                    WalletName = entity.WalletName,
                    Balance = entity.Balance,
                };
                connection.Execute(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(Wallet entity)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                string sql = "usp_UpdateWallet";
                var param = new
                {
                    WalletID = entity.WalletID,
                    UserID = entity.UserID,
                    WalletName = entity.WalletName,
                    Balance = entity.Balance,
                };


                try
                {
                    int result = connection.Execute(sql, param, commandType: CommandType.StoredProcedure);
                    if (result != 2)
                    {
                        throw new Exception("Berhasil melakukan update wallet");
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

        public void Delete(int walletID)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                string sql = "usp_DeleteWallet";
                var param = new { WalletID = walletID };
                connection.Execute(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

        public decimal GetTotalBalance(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                var strSql = "GetTotalWallet";
                var param = new { UserID = id };

                try
                {
                    decimal result = connection.QuerySingleOrDefault<decimal>(strSql, param, commandType: CommandType.StoredProcedure);
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
    }
}
