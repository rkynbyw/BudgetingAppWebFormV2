using BudgetingApp.APIMVC.Models;
using BudgetingApp.APIMVC.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace BudgetingApp.APIMVC.Controllers
{
	////[Route("api/[controller]")]
	//[ApiController]
	public class UsersController : Controller
	{
		private readonly IUserService _userService;
		private readonly ILogger<UsersController> _logger;
		public UsersController(IUserService userService, ILogger<UsersController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		public IActionResult Login()
		{
			if (TempData["Message"] != null)
			{
				ViewBag.Message = TempData["Message"];
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLogin loginDTO)
		{
			try
			{
				var userWithToken = await _userService.Login(loginDTO);
				if (userWithToken == null)
				{
					return RedirectToAction("Login", "Users"); // Ganti dengan action yang benar jika perlu
				}
				var tokenSerializer = JsonSerializer.Serialize(userWithToken);
				HttpContext.Session.SetString("user", tokenSerializer);

				// var userLogin = JsonConvert.DeserializeObject<UserDTO>(Context.Session.GetString("user"));
				// var userRole = userLogin.Role;

				if (userWithToken.Role == "admin")
				{
					return RedirectToAction("GetUsersWithRoles", "Users"); // Ganti dengan action yang benar jika perlu

				}
				else
				{
					ViewBag.ErrorMessage = "Please login using Mobile Apps";
					return View();

				}


				//var userDtoSerialize = JsonSerializer.Serialize(userDto);
				//HttpContext.Session.SetString("user", userDtoSerialize);
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = ex.Message;
				return View();
			}
		}



		public async Task<IActionResult> GetUsersWithRoles()
		{

			var users = await _userService.GetUserWithRoles();
			var listusers = new SelectList(users, "UserID", "Username");
			ViewBag.Users = listusers;


			var roles = await _userService.GetRoles();
			var listRoles = new SelectList(roles, "Role", "Role");
			ViewBag.Roles = listRoles;

			try
			{
				var models = await _userService.GetUserWithRoles();
				return View(models);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> UpdateUserRole(int userId, string role)
		{
			// Check if the user is logged in
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
			{
				return RedirectToAction("Login", "Users");
			}

			if (userId == 0 || string.IsNullOrEmpty(role))
			{
				ModelState.AddModelError("", "Please select a user and a role.");
				return RedirectToAction("GetUsersWithRoles");
			}

			try
			{
				// Retrieve token from session
				var token = HttpContext.Session.GetString("user");
				if (string.IsNullOrEmpty(token))
				{
					return RedirectToAction("Login", "Users");
				}

				// Deserialize the token to get user information
				var userWithToken = JsonSerializer.Deserialize<UserWithToken>(token);
				// Get the token from the userWithToken object
				token = userWithToken.Token;

				await _userService.UpdateRole(userId, role, token);
				ViewBag.SuccessMessage = "User role updated successfully.";
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", $"Failed to update user role: {ex.Message}");
			}

			return RedirectToAction("GetUsersWithRoles");
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("user");
			return RedirectToAction("Login", "Users");
		}



	}
}
