using BudgetingAPIService.BLL.DTOs;
using BudgetingAPIService.BLL.Interfaces;
using BudgetingApp.BO;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;
using Helper = BudgetingAPIService.BLL.Interfaces.Helper;

namespace BudgetingAPIService.BLL
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
                    FullName = entity.FullName,
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
                    Role = result.Role,
                };

                return userDTO;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public UserDTO LoginMVC(UserLoginDTO userLoginDTO)
        {
            if (string.IsNullOrEmpty(userLoginDTO.Username))
            {
                throw new ArgumentNullException("Username is required");
            }
            if (string.IsNullOrEmpty(userLoginDTO.Password))
            {
                throw new ArgumentNullException("Password is required");
            }
            try
            {
                var result = _userDAL.Login(userLoginDTO.Username, Helper.GetHash(userLoginDTO.Password));
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
                    Role = result.Role
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
            var users = _userDAL.GetAll();

            var userDTOs = users.Select(user => new UserDTO
            {
                UserID = user.UserID,
                Email = user.Email,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            });

            return userDTOs;
        }

        public UserDTO GetById(int id)
        {
            var user = _userDAL.GetById(id);
            if (user == null)
            {
                throw new ArgumentException("User not Found");
            }
            UserDTO userDTO = new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role
            };

            return userDTO;
        }

        public IEnumerable<UserDTO> GetRole()
        {
            var roles = _userDAL.GetRole();
            var userDTOs = roles.Select(user => new UserDTO
            {
                Role = user.Role
            });
            return userDTOs;
        }

        public void UpdateRole(int userId, string role)
        {
            try
            {
                _userDAL.UpdateRole(userId, role);
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                throw new Exception($"Failed to update user role: {ex.Message}", ex);
            }
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
