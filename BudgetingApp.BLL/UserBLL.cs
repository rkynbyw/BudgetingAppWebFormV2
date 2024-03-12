using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using BudgetingApp.BO;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BudgetingApp.BLL


{
    public class UserBLL : IUserBLL
    {

        private readonly IUserDAL _userDAL;

        public UserBLL()
        {
            _userDAL = new UserDAL();
        }

        public void Insert(UserCreateDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Username))
            {
                throw new ArgumentNullException("Username is required");
            }
            if (string.IsNullOrEmpty(entity.Password))
            {
                throw new ArgumentNullException("Password is required");
            }
            if (string.IsNullOrEmpty(entity.RePassword))
            {
                throw new ArgumentNullException("Password Confirmation is required");
            }
            if (string.IsNullOrEmpty(entity.Email))
            {
                throw new ArgumentNullException("Password is required");
            }
            if (string.IsNullOrEmpty(entity.FullName))
            {
                throw new ArgumentNullException("Full Name is required");
            }
            if (entity.Password != entity.RePassword)
            {
                throw new ArgumentException("Password and Re-Password must be same");
            }
            try
            {
                var newUser = new User
                {
                    Username = entity.Username,
                    Password = Helper.GetHash(entity.Password),
                    Email = entity.Email,
                    FullName = entity.FullName
                };
                _userDAL.Insert(newUser);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("2627"))
                {
                    throw new ArgumentException("Username already exists");
                }

                throw new ArgumentException(ex.Message);
            }
        }

        public UserDTO Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("Username is required");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password is required");
            }
            try
            {
                var result = _userDAL.Login(username, Helper.GetHash(password));
                if (result == null)
                {
                    throw new ArgumentException("Username atau Password salah");
                }
                UserDTO userDTO = new UserDTO
                {
                    UserID = result.UserID,
                    Username = result.Username,
                    Email = result.Email,
                    FullName = result.FullName,
                };

                return userDTO;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void ChangePassword(string username, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void Delete(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password, string salt)
        {
            throw new NotImplementedException();
        }



        public bool ValidateUserLogin(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
