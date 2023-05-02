using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWithDB_Identity.Models;
using MVCWithDB_Identity.Services;

namespace MVCWithDB_Identity.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: CategoryController
        BaseService<Category> services = new BaseService<Category>();
        [Authorize(Roles = "Admin,Editor,User,Guest")]
        public ActionResult Index() => View(services.List());

        // GET: CategoryController/Details/5
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult Details(int id) => View(services.GetEntity(id));

        // GET: CategoryController/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create() => View();

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                services.AddEntity(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int id) => View(services.GetEntity(id));

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            if (id != category.CategoryId) return NotFound();

            try
            {
                services.UpdateEntity(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id) => View(services.GetEntity(id));

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOK(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                services.RemoveEntity(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
