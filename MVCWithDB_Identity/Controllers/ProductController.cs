using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWithDB_Identity.Models;
using MVCWithDB_Identity.Services;

namespace MVCWithDB_Identity.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: ProductController
        BaseService<Product> services = new BaseService<Product>();
        BaseService<Category> Cservices = new BaseService<Category>();
        BaseService<Supplier> Sservices = new BaseService<Supplier>();

        [Authorize(Roles = "Admin,Editor,User,Guest")]
        public ActionResult Index()
        {
            //var products = from p in services.List()
            //               join c in Cservices.List() on p.CategoryId equals c.CategoryId
            //               join s in Sservices.List() on p.SupplierId equals s.SupplierId
            //               select new Product
            //               {
            //                   CategoryId = p.CategoryId,
            //                   Discontinued = p.Discontinued,
            //                   ProductId = p.ProductId,
            //                   ProductName = p.ProductName,
            //                   QuantityPerUnit = p.QuantityPerUnit,
            //                   ReorderLevel = p.ReorderLevel,
            //                   SupplierId = p.SupplierId,
            //                   UnitPrice = p.UnitPrice,
            //                   UnitsInStock = p.UnitsInStock,
            //                   UnitsOnOrder = p.UnitsOnOrder,
            //                   Category = c,
            //                   Supplier = s
            //               };

            //var products = services.List().Join(Cservices.List(), p => p.CategoryId, c => c.CategoryId, (p, c) => new Product
            //{
            //    CategoryId = p.CategoryId,
            //    Discontinued = p.Discontinued,
            //    ProductId = p.ProductId,
            //    ProductName = p.ProductName,
            //    QuantityPerUnit = p.QuantityPerUnit,
            //    ReorderLevel = p.ReorderLevel,
            //    SupplierId = p.SupplierId,
            //    UnitPrice = p.UnitPrice,
            //    UnitsInStock = p.UnitsInStock,
            //    UnitsOnOrder = p.UnitsOnOrder,
            //    Category = c
            //});

            var products = services.List().Include(x => x.Category).Include(y => y.Supplier).Select(p => new Product
            {
                CategoryId = p.CategoryId,
                Discontinued = p.Discontinued,
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                QuantityPerUnit = p.QuantityPerUnit,
                ReorderLevel = p.ReorderLevel,
                SupplierId = p.SupplierId,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                Category = p.Category,
                Supplier = p.Supplier
            });


            return View(products);
        }

        // GET: ProductController/Details/5
        [Authorize(Roles = "Admin,Editor,User")]
        public ActionResult Details(int id)
        {
            return View(services.GetEntity(id));
        }

        // GET: ProductController/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = from c in Cservices.List() select new SelectListItem { Text = c.CategoryName, Value = c.CategoryId.ToString() };
            ViewBag.SupplierId = from s in Sservices.List() select new SelectListItem { Text = s.CompanyName, Value = s.SupplierId.ToString() };
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                services.AddEntity(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int id)
        {
            //ViewBag.CategoryId = from c in Cservices.List() select new SelectListItem { Text = c.CategoryName, Value = c.CategoryId.ToString() };

            ViewBag.CategoryId = Cservices.List().Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryId.ToString() });
            ViewBag.SupplierId = from s in Sservices.List() select new SelectListItem { Text = s.CompanyName, Value = s.SupplierId.ToString() };

            return View(services.GetEntity(id));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            try
            {
                services.UpdateEntity(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id) => View(services.GetEntity(id));

        // POST: ProductController/Delete/5
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
