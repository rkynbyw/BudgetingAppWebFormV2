using BudgetingApp.APIMVC.Models;

namespace BudgetingApp.APIMVC.Service
{
	public interface IUserService
	{
		Task<UserWithToken> Login(UserLogin loginDTO);
		Task<IEnumerable<UserDTO>> GetUserWithRoles();
		Task UpdateRole(int userId, string role, string token);

		Task<IEnumerable<UserDTO>> GetRoles();
	}
}
