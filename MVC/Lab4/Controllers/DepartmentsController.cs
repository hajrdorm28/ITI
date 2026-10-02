using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        private const string RecentDepartmentsSessionKey = "RecentDepartments";
        private const string LastVisitedDepartmentIdSessionKey = "LastVisitedDepartmentId";
        private const int MaxRecentDepartments = 5;

        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        // Part 3.2 / Bonus 4: records a department visit in Session (name list + last-visited id).
        private void TrackDepartmentVisit(int departmentId, string departmentName)
        {
            var json = HttpContext.Session.GetString(RecentDepartmentsSessionKey);
            var recent = string.IsNullOrEmpty(json)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();

            recent.RemoveAll(n => string.Equals(n, departmentName, StringComparison.OrdinalIgnoreCase));
            recent.Insert(0, departmentName);
            if (recent.Count > MaxRecentDepartments)
            {
                recent = recent.Take(MaxRecentDepartments).ToList();
            }

            HttpContext.Session.SetString(RecentDepartmentsSessionKey, JsonSerializer.Serialize(recent));
            HttpContext.Session.SetInt32(LastVisitedDepartmentIdSessionKey, departmentId);
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .Select(d => new DepartmentIndexViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ManagerName = d.ManagerName,
                    EmployeeCount = d.Employees.Count
                })
                .ToListAsync();

            var recentJson = HttpContext.Session.GetString(RecentDepartmentsSessionKey);
            var recent = string.IsNullOrEmpty(recentJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(recentJson) ?? new List<string>();

            var vm = new DepartmentListViewModel
            {
                Departments = departments,
                RecentlyVisited = recent,
                LastVisitedDepartmentId = HttpContext.Session.GetInt32(LastVisitedDepartmentIdSessionKey)
            };

            return View(vm);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View(new DepartmentViewModel());
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel vm)
        {
            // Special Requirement 1: no two departments with the same name (case-insensitive)
            if (!string.IsNullOrWhiteSpace(vm.Name))
            {
                bool nameExists = await _context.Departments
                    .AnyAsync(d => d.Name.ToLower() == vm.Name.ToLower());

                if (nameExists)
                {
                    ModelState.AddModelError(nameof(vm.Name), "A department with this name already exists.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var department = new Department
            {
                Name = vm.Name,
                ManagerName = vm.ManagerName
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            var vm = new DepartmentViewModel
            {
                Id = department.Id,
                Name = department.Name,
                ManagerName = department.ManagerName
            };

            return View(vm);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (!string.IsNullOrWhiteSpace(vm.Name))
            {
                bool nameExists = await _context.Departments
                    .AnyAsync(d => d.Id != id && d.Name.ToLower() == vm.Name.ToLower());

                if (nameExists)
                {
                    ModelState.AddModelError(nameof(vm.Name), "A department with this name already exists.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            department.Name = vm.Name;
            department.ManagerName = vm.ManagerName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null) return NotFound();

            var vm = new DepartmentIndexViewModel
            {
                Id = department.Id,
                Name = department.Name,
                ManagerName = department.ManagerName,
                EmployeeCount = department.Employees.Count
            };

            return View(vm);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Employees/5  ("Show Employees" button)
        public async Task<IActionResult> Employees(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            // Part 3.2: store the department name in Session whenever this page is opened.
            TrackDepartmentVisit(department.Id, department.Name);

            var employees = await _context.Employees
                .Where(e => e.DepartmentId == id)
                .Select(e => new EmployeeIndexViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    JobTitle = e.JobTitle,
                    DepartmentName = department.Name
                })
                .ToListAsync();

            ViewBag.DepartmentName = department.Name;
            ViewBag.DepartmentId = department.Id;

            return View(employees);
        }
    }
}
