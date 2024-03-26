using BudgetingAPIService.BLL.DTOs;

namespace BudgetingAPIService.BLL.Interfaces
{
    public interface IUserBLL
    {
        void ChangePassword(string username, string newPassword);
        void Delete(string username);
        IEnumerable<UserDTO> GetAll();
        UserDTO GetByUsername(string username);

        UserDTO GetById(int id);
        void Insert(UserCreateDTO entity);
        UserDTO Login(string username, string password);
        UserDTO LoginMVC(UserLoginDTO userLoginDTO);

        void UpdateRole(int userId, string role);
        IEnumerable<UserDTO> GetRole();
    }
}
