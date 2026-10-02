using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        private static readonly int[] AllowedPageSizes = { 5, 10, 20 };

        // GET: Employees
        // Part 6: search by employee name, persisted in a cookie across refreshes.
        // Bonus 6: page size (5/10/20) chosen by the user, persisted in a cookie.
        public async Task<IActionResult> Index(string search, int? pageSize, int page = 1)
        {
            // --- Search term: use the value passed in, otherwise fall back to the cookie ---
            if (search == null)
            {
                search = Request.Cookies["EmployeeSearch"];
            }
            Response.Cookies.Append("EmployeeSearch", search ?? string.Empty, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30)
            });

            // --- Page size: use the value passed in (if valid), otherwise fall back to the cookie, otherwise default to 5 ---
            int effectivePageSize;
            if (pageSize.HasValue && AllowedPageSizes.Contains(pageSize.Value))
            {
                effectivePageSize = pageSize.Value;
            }
            else if (int.TryParse(Request.Cookies["EmployeePageSize"], out var cookieSize) && AllowedPageSizes.Contains(cookieSize))
            {
                effectivePageSize = cookieSize;
            }
            else
            {
                effectivePageSize = 5;
            }
            Response.Cookies.Append("EmployeePageSize", effectivePageSize.ToString(), new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30)
            });

            var query = _context.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Name.ToLower().Contains(search.ToLower()));
            }

            var totalCount = await query.CountAsync();
            if (page < 1) page = 1;

            var employees = await query
                .OrderBy(e => e.Name)
                .Skip((page - 1) * effectivePageSize)
                .Take(effectivePageSize)
                .Select(e => new EmployeeIndexViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    JobTitle = e.JobTitle,
                    DepartmentName = e.Department.Name
                })
                .ToListAsync();

            var vm = new EmployeeListViewModel
            {
                Employees = employees,
                SearchTerm = search,
                PageSize = effectivePageSize,
                PageNumber = page,
                TotalCount = totalCount
            };

            return View(vm);
        }

        // Helper: (re)populate the Departments dropdown.
        // Special Requirement 4: must be called again whenever we return a view after
        // a failed ModelState check, otherwise the dropdown will render empty.
        private async Task PopulateDepartmentsAsync(EmployeeViewModel vm)
        {
            vm.Departments = await _context.Departments
                .OrderBy(d => d.Name)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToListAsync();
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            var vm = new EmployeeViewModel();
            await PopulateDepartmentsAsync(vm);
            return View(vm);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel vm)
        {
            // Special Requirement 2: no two employees with the same name in the same department
            if (!string.IsNullOrWhiteSpace(vm.Name))
            {
                bool duplicateExists = await _context.Employees.AnyAsync(e =>
                    e.DepartmentId == vm.DepartmentId &&
                    e.Name.ToLower() == vm.Name.ToLower());

                if (duplicateExists)
                {
                    ModelState.AddModelError(nameof(vm.Name),
                        "An employee with this name already exists in the selected department.");
                }
            }

            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsAsync(vm);
                return View(vm);
            }

            var employee = new Employee
            {
                Name = vm.Name,
                Age = vm.Age,
                Salary = vm.Salary,
                JobTitle = vm.JobTitle,
                DepartmentId = vm.DepartmentId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            var vm = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                JobTitle = employee.JobTitle,
                DepartmentId = employee.DepartmentId
            };

            await PopulateDepartmentsAsync(vm);

            return View(vm);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (!string.IsNullOrWhiteSpace(vm.Name))
            {
                bool duplicateExists = await _context.Employees.AnyAsync(e =>
                    e.Id != id &&
                    e.DepartmentId == vm.DepartmentId &&
                    e.Name.ToLower() == vm.Name.ToLower());

                if (duplicateExists)
                {
                    ModelState.AddModelError(nameof(vm.Name),
                        "An employee with this name already exists in the selected department.");
                }
            }

            if (!ModelState.IsValid)
            {
                await PopulateDepartmentsAsync(vm);
                return View(vm);
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            employee.Name = vm.Name;
            employee.Age = vm.Age;
            employee.Salary = vm.Salary;
            employee.JobTitle = vm.JobTitle;
            employee.DepartmentId = vm.DepartmentId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return NotFound();

            var vm = new EmployeeIndexViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                JobTitle = employee.JobTitle,
                DepartmentName = employee.Department.Name
            };

            return View(vm);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
