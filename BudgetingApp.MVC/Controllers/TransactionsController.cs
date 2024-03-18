using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace BudgetingApp.MVC.Controllers
{
	public class TransactionsController : Controller
	{
		private readonly ITransactionBLL _transactionBLL;
		private readonly ITransactionCategoryBLL _transactionCategoryBLL;
		private readonly IWalletBLL _walletBLL;

		public TransactionsController(ITransactionBLL transactionBLL, IWalletBLL walletBLL, ITransactionCategoryBLL transactionCategoryBLL)
		{
			_transactionBLL = transactionBLL;
			_transactionCategoryBLL = transactionCategoryBLL;
			_walletBLL = walletBLL;
		}

		// GET: TransactionsController
		//public ActionResult Index()
		public ActionResult Index(int? year, int? month, int? transactionCategoryID, int? transactionTypeID)
		{
			var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
			ViewBag.role = userDto.Role;
			ViewBag.userid = userDto.UserID;
			var userId = userDto.UserID;

			//IEnumerable<TransactionDTO> models;
			//models = _transactionBLL.GetUserTransaction(userDto);
			IEnumerable<TransactionDTO> models = _transactionBLL.GetUserTransactionV2(userId, year, month, transactionCategoryID, transactionTypeID);


			decimal totalExpense = models.Where(t => t.TransactionCategory.TransactionTypeID == 1).Sum(t => t.Amount);
			decimal totalIncome = models.Where(t => t.TransactionCategory.TransactionTypeID == 2).Sum(t => t.Amount);

			ViewBag.TotalExpense = totalExpense;
			ViewBag.TotalIncome = totalIncome;


			var transactionTypes = new List<SelectListItem>
			{
				new SelectListItem { Text = "Pengeluaran", Value = "1" },
				new SelectListItem { Text = "Pemasukan", Value = "2" }
			};
			ViewBag.TransactionTypes = transactionTypes;

			return View(models);
		}

		// GET: TransactionsController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: TransactionsController/Create
		public ActionResult Create()
		{
			var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
			var transaction = new TransactionDTO
			{
				UserID = userDto.UserID,
				Date = DateTime.Today
			};

			// Sisipkan model ke ViewBag jika Anda memerlukan data tambahan di view
			ViewBag.role = userDto.Role;
			ViewBag.userid = userDto.UserID;
			var userId = userDto.UserID;

			// Set ViewBag untuk TransactionTypes
			var transactionTypes = new List<SelectListItem>
	{
		new SelectListItem { Text = "Pengeluaran", Value = "1" },
		new SelectListItem { Text = "Pemasukan", Value = "2" }
	};
			ViewBag.TransactionTypes = transactionTypes;

			// Default untuk TransactionCategoryID, karena belum ada pilihan TransactionType yang dipilih
			ViewBag.TransactionCategories = new List<SelectListItem>();

			// Default untuk Wallets, Anda bisa mengubah nilai default ini jika diperlukan
			ViewBag.Wallets = _walletBLL.GetWalletDataByUser(userId).Select(w => new SelectListItem
			{
				Text = w.WalletName,
				Value = w.WalletID.ToString()
			});

			return View(transaction);
		}


		public IActionResult GetCategoryByType(int transactionTypeId)
		{
			var categories = _transactionCategoryBLL.GetCategoryNameByType(transactionTypeId);
			var result = categories.Select(c => new { value = c.TransactionCategoryID, text = c.Name });
			return Json(result);
		}


		// POST: TransactionsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(TransactionDTO transactionDTO)
		{
			if (ModelState.IsValid)
			{
				_transactionBLL.Insert(transactionDTO);
				return RedirectToAction(nameof(Index));
			}
			return View(transactionDTO);
		}

		public ActionResult Edit(int id)
		{
			var transaction = _transactionBLL.GetById(id);
			if (transaction == null)
			{
				return NotFound();
			}

			PopulateDropdowns(transaction);

			return View(transaction);
		}

		[HttpPost]
		public IActionResult Edit(int id, TransactionDTO transaction)
		{
			if (id != transaction.TransactionID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_transactionBLL.Update(transaction);
					TempData["message"] = "<div class='alert alert-success'><strong>Success!</strong> Edit Data Transaction Success !</div>";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");
					PopulateDropdowns(transaction); // Populate dropdowns again if there's a validation error
					return View(transaction);
				}
			}

			PopulateDropdowns(transaction); // Populate dropdowns again if there's a validation error
			return View(transaction);
		}

		private void PopulateDropdowns(TransactionDTO transaction)
		{

			var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
			ViewBag.role = userDto.Role;
			ViewBag.userid = userDto.UserID;
			var userId = userDto.UserID;

			ViewBag.Wallets = _walletBLL.GetWalletDataByUser(userId)
				.Select(w => new SelectListItem { Text = w.WalletName, Value = w.WalletID.ToString() });

			if (transaction.TransactionCategoryID >= 1 && transaction.TransactionCategoryID <= 6)
			{
				transaction.TransactionType = 1; // Pengeluaran
			}
			else if (transaction.TransactionCategoryID >= 7 && transaction.TransactionCategoryID <= 9)
			{
				transaction.TransactionType = 2; // Pemasukan
			}
			else
			{
				transaction.TransactionType = 0; // Default value
			}

			ViewBag.TransactionCategories = _transactionCategoryBLL.GetCategoryNameByType(transaction.TransactionType)
				.Select(c => new SelectListItem { Text = c.Name, Value = c.TransactionCategoryID.ToString(), Selected = c.TransactionCategoryID == transaction.TransactionCategoryID });

			ViewBag.TransactionType = transaction.TransactionType.ToString();
			ViewBag.TransactionCategoryID = transaction.TransactionCategoryID.ToString();
		}


		// GET: TransactionsController/Delete/5
		public ActionResult Delete(int id)
		{
			var transaction = _transactionBLL.GetById(id);
			return View(transaction);
		}

		// POST: TransactionsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, TransactionDTO transactionDTO)
		{
			try
			{
				_transactionBLL.Delete(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
