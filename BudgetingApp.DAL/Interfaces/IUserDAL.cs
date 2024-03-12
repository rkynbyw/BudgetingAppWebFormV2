using BudgetingApp.BO;

namespace BudgetingApp.DAL.Interfaces
{
    public interface IUserDAL : Icrud<User>
    {
        User GetUserByEmail(string email);
        User Login(string username, string password);
    }
}
