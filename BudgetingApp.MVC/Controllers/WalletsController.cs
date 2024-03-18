using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetingApp.MVC.Controllers
{
    public class WalletsController : Controller
    {
        // GET: WalletsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WalletsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalletsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalletsController/Create
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

        // GET: WalletsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalletsController/Edit/5
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

        // GET: WalletsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalletsController/Delete/5
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
