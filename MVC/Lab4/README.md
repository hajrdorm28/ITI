# Employee Management System — Lab 3 + Lab 4

ASP.NET Core 8 MVC + EF Core. Lab 3 added ViewModels, custom validation,
async CRUD, and jQuery unobtrusive client-side validation. Lab 4 adds custom
middleware, cookies, sessions, search/paging, a dashboard, and error handling
on top of it — all previous features still work.

## How to run

1. **Requirements**: .NET 8 SDK, and either SQL Server LocalDB (comes with
   Visual Studio) or full SQL Server/Azure SQL.
2. Open a terminal in the project folder and install EF tooling (once):
   ```
   dotnet tool install --global dotnet-ef
   ```
3. Create the database and apply migrations:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Run the app:
   ```
   dotnet run
   ```
5. Browse to the URL shown in the console (e.g. `https://localhost:5001`).
   The app opens on the **Home** page.
6. Click **Login** (top right) before trying to open **Departments** or
   **Employees** — those two areas are now protected by session-based auth
   (see Bonus 1 below). Any username is accepted; there's no password by
   design, since Lab 4 asks for a "simple login simulation."

### No SQL Server available?

Swap the provider to SQLite (zero setup) — same steps as Lab 3:
1. Drop the `SqlServer` package reference in the `.csproj` (the `Sqlite` one
   is already included).
2. In `Program.cs`, change `UseSqlServer(...)` to `UseSqlite(...)`.
3. In `appsettings.json`, set `"DefaultConnection": "Data Source=employees.db"`.
4. Re-run the `dotnet ef migrations add` / `dotnet ef database update` commands.

## Project structure

```
Controllers/
  AccountController.cs       - Login/Logout (Session), RememberName + Theme (Cookies), maintenance toggle
  DepartmentsController.cs   - async CRUD, "Show Employees", recent-department Session tracking
  EmployeesController.cs     - async CRUD, search + paging (Cookie-persisted), duplicate checks
  HomeController.cs          - Home (welcome/cookie + visit counter/session), Dashboard, Error
Middleware/
  RequestLoggingMiddleware.cs   - Part 1.1 / 1.3: logs path/method/time, before+after next()
  MaintenanceModeMiddleware.cs  - Part 1.2: short-circuits the pipeline when AppState.MaintenanceModeEnabled
  RequestTimingMiddleware.cs    - Bonus 5: logs how long each request took
  RequestCounterMiddleware.cs   - Bonus 2: counts total requests since startup
  AuthCheckMiddleware.cs        - Bonus 1: blocks /Employees and /Departments unless logged in
Services/
  AppState.cs                - Singleton: maintenance flag + running request counter
Data/
  AppDbContext.cs            - EF Core DbContext, seed data
Models/
  Department.cs, Employee.cs - EF entities (never exposed directly to views)
ViewModels/
  DepartmentViewModel.cs / EmployeeViewModel.cs   - Add/Edit form models + validation
  DepartmentIndexViewModel.cs / EmployeeIndexViewModel.cs - row display models
  DepartmentListViewModel.cs / EmployeeListViewModel.cs   - Index page wrappers (list + search/paging/recent-visits)
  LoginViewModel.cs / RememberNameViewModel.cs    - Account forms
  DashboardViewModel.cs      - statistics page
Validation/
  MinimumAgeIfHighSalaryAttribute.cs  - custom rule: age >= 22 if salary > 20000
  NoNumbersInNameAttribute.cs         - custom rule: name must not contain digits
Views/
  Account/      Login, RememberName
  Departments/  Index, Create, Edit, Delete, Employees
  Employees/    Index, Create, Edit, Delete
  Home/         Index, Dashboard
  Shared/       _Layout, _ValidationScriptsPartial, Error
```

## Lab 4 requirement checklist → where it's implemented

| Requirement | Where |
|---|---|
| 1.1 Request logger middleware | `Middleware/RequestLoggingMiddleware.cs` |
| 1.2 Maintenance middleware | `Middleware/MaintenanceModeMiddleware.cs`, toggled via `Account/ToggleMaintenance`, flag lives in `Services/AppState.cs` |
| 1.3 Execution order (before/after `next()`) | Also in `RequestLoggingMiddleware` — prints a line before and after `await _next(context)` |
| 2.1 Remember username (Cookie) | `AccountController.RememberName` (GET/POST) writes cookie `RememberedUserName` |
| 2.2 Welcome message | `HomeController.Index` reads the cookie and sets `ViewBag.WelcomeName` |
| 2.3 Preferred theme (Cookie) | `AccountController.SetTheme` writes cookie `Theme`; `_Layout.cshtml` reads it on every page |
| 3.1 Visit counter (Session) | `HomeController.Index` increments `Session["VisitCount"]` |
| 3.2 Recently visited departments (Session) | `DepartmentsController.TrackDepartmentVisit`, called from the `Employees` ("Show Employees"/Details) action; shown on `Departments/Index` |
| 3.3 Logged-in user simulation (Session) | `AccountController.Login` stores `Session["Username"]`; shown in `_Layout` navbar |
| 3.4 Logout | `AccountController.Logout` clears the session and redirects to Login |
| 4/5 Employee & Department modules | `Employees/Index.cshtml`, `Departments/Index.cshtml` (columns/buttons per spec) |
| 6 Search employees by name (Cookie-persisted) | `EmployeesController.Index(search, ...)`, cookie `EmployeeSearch` |
| 7 ViewModels + Data annotation + client/server validation | Carried over from Lab 3, still enforced everywhere |
| 8 Dashboard (async stats) | `HomeController.Dashboard` — `CountAsync`/`AverageAsync`/`MaxAsync`/`MinAsync` |
| 9 Custom error page | `Views/Shared/Error.cshtml` + `app.UseExceptionHandler("/Home/Error")` in `Program.cs`; try `Home/ForceError` to see it trigger |
| Bonus 1: auth-gate Employees/Departments | `Middleware/AuthCheckMiddleware.cs` |
| Bonus 2: total request counter | `Middleware/RequestCounterMiddleware.cs` + `Services/AppState.cs`, shown in the page footer |
| Bonus 3: last login date (Cookie) | `AccountController.Login` reads cookie `LastLoginDate` into `TempData` before overwriting it |
| Bonus 4: highlight last-visited department | `Departments/Index.cshtml` — row gets `table-warning` class + a "Last visited" badge |
| Bonus 5: request timing middleware | `Middleware/RequestTimingMiddleware.cs` |
| Bonus 6: page size selection (Cookie) | `EmployeesController.Index(pageSize, ...)`, cookie `EmployeePageSize`, dropdown on `Employees/Index.cshtml` |
| Bonus 7: username in navbar | `_Layout.cshtml` reads `Session["Username"]` on every page |

## Middleware pipeline order (`Program.cs`)

```
UseExceptionHandler          -> catches unhandled exceptions anywhere below
UseHttpsRedirection
UseMaintenanceMode            (custom)  -> can short-circuit everything below
UseStaticFiles
UseRouting
UseSession                    -> must come before anything reading/writing Session
UseRequestCounter              (custom)
UseRequestTiming                (custom)
UseRequestLogging               (custom)
UseAuthCheck                     (custom)  -> needs Session, must run before endpoints execute
UseAuthorization
MapControllerRoute (default: Home/Index)
```

## Notes

- There's no real password — logging in just records the chosen username in
  Session, matching the lab's "simple login page / simulation" wording.
- To demo maintenance mode: visit `/Account/ToggleMaintenance` once to turn
  it on, browse anywhere to see the maintenance page, then visit the same
  URL again to turn it back off.
- Console output (from the four custom middlewares) is visible in the
  terminal running `dotnet run` — that's where the "before/after `next()`"
  and per-request timing lines show up.
- `ModelState.IsValid` is always checked in POST actions before touching the
  database, and the Employees dropdown is always repopulated before
  returning a view after a failed validation (Lab 3 requirement, still true).
