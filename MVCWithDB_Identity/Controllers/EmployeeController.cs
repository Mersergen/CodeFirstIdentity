using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWithDB_Identity.Models;
using MVCWithDB_Identity.Services;

namespace MVCWithDB_Identity.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        BaseService<Employee> services = new BaseService<Employee>();
        [Authorize(Roles = "Admin,Editor,User,Guest")]
        public ActionResult Index() => View(services.List());

        // GET: EmployeeController/Details/5
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult Details(int id) => View(services.GetEntity(id));

        // GET: EmployeeController/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create() => View();

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                services.AddEntity(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employee);
            }
        }

        // GET: EmployeeController/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int id) => View(services.GetEntity(id));

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId) return NotFound();

            try
            {
                services.UpdateEntity(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employee);
            }
        }

        // GET: EmployeeController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id) => View(services.GetEntity(id));

        // POST: EmployeeController/Delete/5
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
