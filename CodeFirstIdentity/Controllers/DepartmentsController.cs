using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFirstIdentity.Models;
using Microsoft.AspNetCore.Authorization;

namespace CodeFirstIdentity.Controllers
{
	public class DepartmentsController : Controller
	{
		private readonly Context _context;

		public DepartmentsController(Context context)
		{
			_context = context;
		}

		// GET: Departments
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			return _context.Departments != null ?
						View(await _context.Departments.ToListAsync()) :
						Problem("Entity set 'Context.Departments'  is null.");
		}

		// GET: Departments/Details/5
		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Departments == null)
			{
				return NotFound();
			}

			var department = await _context.Departments
				.FirstOrDefaultAsync(m => m.ID == id);
			if (department == null)
			{
				return NotFound();
			}

			return View(department);
		}

		// GET: Departments/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Departments/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,DepartmentName,Phone,Mail")] Department department)
		{
			try
			{
				_context.Add(department);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				throw new Exception("Ekleme yaparken Hata Oluştu");
			}

		}

		// GET: Departments/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Departments == null)
			{
				return NotFound();
			}

			var department = await _context.Departments.FindAsync(id);
			if (department == null)
			{
				return NotFound();
			}
			return View(department);
		}

		// POST: Departments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,DepartmentName,Phone,Mail")] Department department)
		{
			if (id != department.ID)
			{
				return NotFound();
			}

			try
			{
				_context.Update(department);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DepartmentExists(department.ID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: Departments/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Departments == null)
			{
				return NotFound();
			}

			var department = await _context.Departments
				.FirstOrDefaultAsync(m => m.ID == id);
			if (department == null)
			{
				return NotFound();
			}

			return View(department);
		}

		// POST: Departments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Departments == null)
			{
				return Problem("Entity set 'Context.Departments'  is null.");
			}
			var department = await _context.Departments.FindAsync(id);
			if (department != null)
			{
				_context.Departments.Remove(department);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool DepartmentExists(int id)
		{
			return (_context.Departments?.Any(e => e.ID == id)).GetValueOrDefault();
		}
	}
}
