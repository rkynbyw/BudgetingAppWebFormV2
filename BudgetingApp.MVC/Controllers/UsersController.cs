using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace BudgetingApp.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBLL _userBLL;

        public UsersController(IUserBLL userBLL)
        {
            _userBLL = userBLL;
        }
        // GET: UsersController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListUser()
        {
            var models = _userBLL.GetAll();

            var users = _userBLL.GetAll();
            var listusers = new SelectList(users, "UserID", "Username");
            ViewBag.Users = listusers;


            var roles = _userBLL.GetRole();
            var listRoles = new SelectList(roles, "Role", "Role");
            ViewBag.Roles = listRoles;

            return View(models);
        }

        [HttpPost]
        public ActionResult UpdateRole(int userId, string role)
        {
            if (userId == 0 || string.IsNullOrEmpty(role))
            {
                ModelState.AddModelError("", "Please select a user and a role.");
                return RedirectToAction("ListUser");
            }

            _userBLL.UpdateRole(userId, role);


            return RedirectToAction("ListUser");
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
        public IActionResult Login(UserLoginDTO userLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var userDto = _userBLL.LoginMVC(userLoginDTO);
                var userDtoSerialize = JsonSerializer.Serialize(userDto);
                HttpContext.Session.SetString("user", userDtoSerialize);


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
                return View();
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserCreateDTO userCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _userBLL.Insert(userCreateDTO);
                return RedirectToAction("Login", "Users");
            }
            catch (Exception ex)
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login", "Users");
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
