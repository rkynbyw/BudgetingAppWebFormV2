using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetingApp.MVC.Controllers
{
    public class TransactionCategoriesController : Controller
    {
        // GET: TransactionCategoriesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionCategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionCategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionCategoriesController/Create
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

        // GET: TransactionCategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionCategoriesController/Edit/5
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

        // GET: TransactionCategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionCategoriesController/Delete/5
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
