using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWithDB_Identity.Models;
using MVCWithDB_Identity.Services;

namespace MVCWithDB_Identity.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        // GET: SupplierController
        BaseService<Supplier> services = new BaseService<Supplier>();
        [Authorize(Roles = "Admin,Editor,User,Guest")]
        public ActionResult Index() => View(services.List());

        // GET: SupplierController/Details/5
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult Details(int id) => View(services.GetEntity(id));

        // GET: SupplierController/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create() => View();

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                services.AddEntity(supplier);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(supplier);
            }
        }

        // GET: SupplierController/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int id) => View(services.GetEntity(id));

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId) return NotFound();

            try
            {
                services.UpdateEntity(supplier);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(supplier);
            }
        }

        // GET: SupplierController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id) => View(services.GetEntity(id));

        // POST: SupplierController/Delete/5
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
