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

            // Create a dictionary to store expenses by category
            var expensesByCategory = new Dictionary<int, decimal>();

            // Iterate through each budget to calculate expenses
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

            // Pass budgets and expenses to the view
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

            // Mendapatkan kategori transaksi yang tersedia
            var transactionCategories = _categoryBLL.GetCategoryNameByType(1)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.TransactionCategoryID.ToString(),
                    Selected = c.TransactionCategoryID == budget.TransactionCategoryID
                });

            // Menyiapkan ViewBag dengan data yang diperlukan
            ViewBag.TransactionCategories = transactionCategories;

            // Menyiapkan ViewBag untuk TransactionCategoryID
            ViewBag.TransactionCategoryID = budget.TransactionCategoryID;

            // Inisialisasi ViewBag.Categories
            ViewBag.Categories = _categoryBLL.GetCategoryNameByType(1); // Ganti dengan metode yang sesuai

            return View(budget);
        }





        // POST: BudgetsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BudgetDTO budgetDTO)
        {
            if (id != budgetDTO.BudgetID)
            {
                return NotFound(); // Return 404 Not Found jika ID tidak cocok dengan ID pada objek budgetDTO
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _budgetBLL.UpdateBudget(budgetDTO); // Memperbarui budget
                    return RedirectToAction("BudgetWithExpense");
                }
                catch
                {
                    // Jika terjadi kesalahan saat memperbarui, kembalikan tampilan edit dengan budgetDTO
                    return View(budgetDTO);
                }
            }
            return View(budgetDTO); // Kembalikan tampilan edit dengan budgetDTO jika ModelState tidak valid
        }

        // GET: BudgetsController/Delete/5
        public ActionResult Delete(int id)
        {
            var budget = _budgetBLL.GetBudgetByID(id);
            if (budget == null)
            {
                return NotFound(); // Return 404 Not Found jika budget dengan ID yang diberikan tidak ditemukan
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
                _budgetBLL.DeleteBudget(id); // Hapus budget dengan ID yang diberikan
                return RedirectToAction("BudgetWithExpense");
            }
            catch
            {
                return View(); // Kembalikan tampilan delete jika terjadi kesalahan
            }
        }

    }
}
