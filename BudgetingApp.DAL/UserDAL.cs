using BudgetingApp.BO;
using BudgetingApp.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BudgetingApp.DAL
{
    public class UserDAL : IUserDAL
    {
        private string GetConnectionString()
        {
            //return ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"].ConnectionString;
            return Helper.GetConnectionString();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"select * from Users order by UserID";
                var results = conn.Query<User>(strSql);
                return results;
            }
        }

        public User GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"select * from Users where UserID = @UserID";
                var param = new { UserID = id };
                var result = conn.QuerySingleOrDefault<User>(strSql, param);
                return result;
            }

        }

        public User GetUserByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {

            }

            return null;
        }

        public void Insert(User entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    var strSql = @"usp_CreateUser";
                    var param = new
                    {

                        Email = entity.Email,
                        Password = entity.Password,
                        Username = entity.Username,
                        Fullname = entity.FullName,

                    };

                    int result = conn.Execute(strSql, param, commandType: System.Data.CommandType.StoredProcedure);
                    if (result != 4)
                    {
                        throw new SystemException("Pendaftaran user gagal");
                    }
                }


                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Kesalahan: " + ex.Message);
                }
            }
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public User Login(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"select * from Users where Username = @Username and Password = @Password";
                var param = new { Username = username, Password = password };
                var result = conn.QueryFirstOrDefault<User>(strSql, param);
                if (result == null)
                {
                    throw new ArgumentException("Username atau Password salah");
                }
                return result;
            }
        }
        public IEnumerable<User> GetRole()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = "GetRole";
                var results = conn.Query<User>(strSql, commandType: System.Data.CommandType.StoredProcedure);
                return results;
            }
        }

        public void UpdateRole(int userId, string role)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    var strSql = "UpdateRole";
                    var param = new
                    {
                        UserID = userId,
                        Role = role
                    };
                    int result = conn.Execute(strSql, param, commandType: System.Data.CommandType.StoredProcedure);
                    if (result != 1)
                    {
                        throw new SystemException("Update Role gagal");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Kesalahan: " + ex.Message);
                }

            }
        }
    }
}
