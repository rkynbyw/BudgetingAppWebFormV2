using BudgetingApp.BO;
using System.Collections.Generic;

namespace BudgetingApp.DAL.Interfaces
{
	public interface IUserDAL : Icrud<User>
	{
		User GetUserByEmail(string email);
		User Login(string username, string password);

		IEnumerable<User> GetRole();

		void UpdateRole(int userId, string role);
	}
}
