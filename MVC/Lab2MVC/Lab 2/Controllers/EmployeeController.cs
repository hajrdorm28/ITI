using Lab_2.Data;
using Lab_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab_2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "DepartmentName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            if (!ModelState.IsValid) return View(employee);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0) return View();

            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "DepartmentName", emp.DepartmentId);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id) return NotFound();

            if (!ModelState.IsValid) return View(employee);

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return View();

            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
