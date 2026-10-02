using Lab_2.Data;
using Lab_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_2.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .ToListAsync();

            return View(departments);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department dept)
        {
            if(!ModelState.IsValid) return View(dept);

            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0) return View();

            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound();

            return View(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Department dept)
        {
            if (id != dept.Id) return NotFound();

            if (!ModelState.IsValid) return View(dept);

            _context.Departments.Update(dept);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return View();

            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound();

            return View(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dept == null) return NotFound();

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(int? id)
        {
            if (id == null || id <= 0) return View();

            var dept = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dept == null) return NotFound();

            return View(dept);
        }
    }
}
