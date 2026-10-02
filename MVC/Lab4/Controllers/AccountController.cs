using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppState _appState;

        public AccountController(AppState appState)
        {
            _appState = appState;
        }

        // ---------- Part 3.3 / 3.4: Logged-in user simulation (Session) ----------

        // GET: Account/Login
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Bonus 3: read the previous login date (if any) BEFORE overwriting it,
            // so it can be shown to the user after this login completes.
            var previousLogin = Request.Cookies["LastLoginDate"];
            TempData["PreviousLoginDate"] = previousLogin;

            // Part 3.3: store the username in Session.
            HttpContext.Session.SetString("Username", vm.UserName);

            // Bonus 3: store "now" as the new last-login-date cookie for next time.
            Response.Cookies.Append("LastLoginDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });

            if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
            {
                return Redirect(vm.ReturnUrl);
            }

            return RedirectToAction("Index", "Departments");
        }

        // Part 3.4: Logout clears the Session and redirects to Login.
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        // ---------- Part 2.1: Remember User Name (Cookie) ----------

        // GET: Account/RememberName
        public IActionResult RememberName()
        {
            var existing = Request.Cookies["RememberedUserName"];
            return View(new RememberNameViewModel { UserName = existing });
        }

        // POST: Account/RememberName
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RememberName(RememberNameViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Response.Cookies.Append("RememberedUserName", vm.UserName, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30)
            });

            TempData["Message"] = "Your name has been remembered.";
            return RedirectToAction("Index", "Home");
        }

        // ---------- Part 2.3: Preferred Theme (Cookie) ----------

        // POST: Account/SetTheme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetTheme(string theme, string returnUrl = null)
        {
            if (theme != "Dark") theme = "Light"; // whitelist valid values

            Response.Cookies.Append("Theme", theme, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // ---------- Demo helper for Part 1.2 (Maintenance Middleware) ----------

        // GET: Account/ToggleMaintenance
        // Flips the AppState.MaintenanceModeEnabled flag so the maintenance
        // middleware behavior can be demonstrated without editing code.
        public IActionResult ToggleMaintenance()
        {
            _appState.MaintenanceModeEnabled = !_appState.MaintenanceModeEnabled;
            return Content(
                $"Maintenance mode is now: {(_appState.MaintenanceModeEnabled ? "ON" : "OFF")}. " +
                "Go back and refresh any page to see the effect.");
        }
    }
}
