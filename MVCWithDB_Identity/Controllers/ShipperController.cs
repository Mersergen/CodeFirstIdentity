using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWithDB_Identity.Models;
using MVCWithDB_Identity.Services;

namespace MVCWithDB_Identity.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {
        // GET: ShipperController
        BaseService<Shipper> services = new BaseService<Shipper>();
        [Authorize(Roles = "Admin,Editor,User,Guest")]
        public ActionResult Index() => View(services.List());

        // GET: ShipperController/Details/5
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult Details(int id) => View(services.GetEntity(id));

        // GET: ShipperController/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create() => View();

        // POST: ShipperController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shipper shipper)
        {
            try
            {
                services.AddEntity(shipper);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(shipper);
            }
        }

        // GET: ShipperController/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int id) => View(services.GetEntity(id));

        // POST: ShipperController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Shipper shipper)
        {
            if (id != shipper.ShipperId) return NotFound();

            try
            {
                services.UpdateEntity(shipper);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(shipper);
            }
        }

        // GET: ShipperController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id) => View(services.GetEntity(id));

        // POST: ShipperController/Delete/5
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
