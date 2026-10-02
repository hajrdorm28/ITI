using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Part 2.2: Welcome message based on the "RememberedUserName" cookie.
        // Part 3.1: Visit counter using Session, incremented on every refresh.
        public IActionResult Index()
        {
            var rememberedName = Request.Cookies["RememberedUserName"];
            ViewBag.WelcomeName = string.IsNullOrEmpty(rememberedName) ? "Guest" : rememberedName;

            var visits = HttpContext.Session.GetInt32("VisitCount") ?? 0;
            visits++;
            HttpContext.Session.SetInt32("VisitCount", visits);
            ViewBag.VisitCount = visits;

            return View();
        }

        // Part 8: Dashboard / Statistics page - all data retrieved asynchronously.
        public async Task<IActionResult> Dashboard()
        {
            var totalEmployees = await _context.Employees.CountAsync();
            var totalDepartments = await _context.Departments.CountAsync();

            var vm = new DashboardViewModel
            {
                TotalEmployees = totalEmployees,
                TotalDepartments = totalDepartments,
                AverageSalary = totalEmployees == 0 ? 0 : await _context.Employees.AverageAsync(e => e.Salary),
                HighestSalary = totalEmployees == 0 ? 0 : await _context.Employees.MaxAsync(e => e.Salary),
                LowestSalary = totalEmployees == 0 ? 0 : await _context.Employees.MinAsync(e => e.Salary)
            };

            return View(vm);
        }

        // Part 9: custom, friendly error page. Reached via app.UseExceptionHandler("/Home/Error").
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ViewBag.OriginalPath = exceptionFeature?.Path;

            return View();
        }

        // Demo-only action to trigger an unhandled exception, so the custom
        // error page / exception handler middleware can be verified end to end.
        public IActionResult ForceError()
        {
            throw new InvalidOperationException("This is a demo exception to test the custom error page.");
        }
    }
}
