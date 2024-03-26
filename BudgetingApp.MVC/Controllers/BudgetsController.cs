using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace BudgetingApp.MVC.Controllers
{
    public class BudgetsController : Controller
    {
        // GET: BudgetsController
        private readonly IBudgetBLL _budgetBLL;
        private readonly ITransactionBLL _transactionBLL;
        private readonly ITransactionCategoryBLL _categoryBLL;

        public BudgetsController(IBudgetBLL budgetBLL, ITransactionBLL transactionBLL, ITransactionCategoryBLL categoryBLL)
        {
            _budgetBLL = budgetBLL;
            _transactionBLL = transactionBLL;
            _categoryBLL = categoryBLL;
        }
        public ActionResult Index()
        {

            var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
            ViewBag.role = userDto.Role;
            ViewBag.userid = userDto.UserID;
            var userId = userDto.UserID;
            IEnumerable<BudgetDTO> models = _budgetBLL.GetUserBudgets(userId);
            return View(models);
        }

        public ActionResult BudgetWithExpense()
        {
            var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
            ViewBag.role = userDto.Role;
            ViewBag.userid = userDto.UserID;
            var userId = userDto.UserID;

            IEnumerable<BudgetDTO> models = _budgetBLL.GetUserBudgets(userId);


            var expensesByCategory = new Dictionary<int, decimal>();

            foreach (var budget in models)
            {
                if (budget.TransactionCategoryID.HasValue)
                {
                    int categoryId = budget.TransactionCategoryID.Value;
                    decimal expense = _transactionBLL.GetUserExpenseByMonth(userId, budget.MonthDate.Year, budget.MonthDate.Month, categoryId);
                    expensesByCategory[categoryId] = expense;

                    budget.RemainingAmount = budget.Amount - expense;
                }
            }

            ViewBag.Budgets = models;
            ViewBag.ExpensesByCategory = expensesByCategory;

            return View();
        }


        // GET: BudgetsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BudgetsController/Create
        public ActionResult Create()
        {
            var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
            var budget = new BudgetDTO
            {
                UserID = userDto.UserID,
                MonthDate = DateTime.Today
            };

            ViewBag.role = userDto.Role;
            ViewBag.userid = userDto.UserID;
            var userId = userDto.UserID;

            var categories = _categoryBLL.GetCategoryNameByType(1);
            ViewBag.Categories = categories;

            return View(budget);
        }


        // POST: BudgetsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetDTO budgetDTO)
        {
            try
            {
                _budgetBLL.InsertBudget(budgetDTO);
                return RedirectToAction("BudgetWithExpense");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while creating the budget: {ex.Message}");
                ViewBag.Categories = _categoryBLL.GetCategoryNameByType(1);
                ViewBag.Categories = ViewBag.Categories;
                return View(budgetDTO);
            }
        }


        // GET: BudgetsController/Edit/5
        public ActionResult Edit(int id)
        {
            var budget = _budgetBLL.GetBudgetByID(id);
            if (budget == null)
            {
                return NotFound();
            }


            var transactionCategories = _categoryBLL.GetCategoryNameByType(1)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.TransactionCategoryID.ToString(),
                    Selected = c.TransactionCategoryID == budget.TransactionCategoryID
                });

            ViewBag.TransactionCategories = transactionCategories;

            ViewBag.TransactionCategoryID = budget.TransactionCategoryID;

            ViewBag.Categories = _categoryBLL.GetCategoryNameByType(1);

            return View(budget);
        }





        // POST: BudgetsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BudgetDTO budgetDTO)
        {
            if (id != budgetDTO.BudgetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _budgetBLL.UpdateBudget(budgetDTO);
                    return RedirectToAction("BudgetWithExpense");
                }
                catch
                {

                    return View(budgetDTO);
                }
            }
            return View(budgetDTO);
        }

        // GET: BudgetsController/Delete/5
        public ActionResult Delete(int id)
        {
            var budget = _budgetBLL.GetBudgetByID(id);
            if (budget == null)
            {
                return NotFound();
            }
            return View(budget);
        }

        // POST: BudgetsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BudgetDTO budgetDTO)
        {
            try
            {
                _budgetBLL.DeleteBudget(id);
                return RedirectToAction("BudgetWithExpense");
            }
            catch
            {
                return View();
            }
        }

    }
}
