using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Middleware
{
    public class MaintenanceModeMiddleware
    {
        private readonly RequestDelegate _next;

        public MaintenanceModeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppState appState)
        {
            var isToggleRequest = context.Request.Path.StartsWithSegments("/Account/ToggleMaintenance");

            if (appState.MaintenanceModeEnabled && !isToggleRequest)
            {
                context.Response.StatusCode = 503;
                context.Response.ContentType = "text/html";

                await context.Response.WriteAsync($$"""
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <title>Under Maintenance</title>
                        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" />
                    </head>
                    <body class="bg-light">
                        <div class="container text-center" style="margin-top: 100px;">
                            <h1 class="display-5">🛠️ The system is currently under maintenance.</h1>
                            <p class="text-muted">Please check back later.</p>
                            <a href="/Account/ToggleMaintenance" class="btn btn-secondary mt-3">(Demo) Turn maintenance mode off</a>
                        </div>
                    </body>
                    </html>
                    """);

                return; 
            }

            await _next(context);
        }
    }

    public static class MaintenanceModeMiddlewareExtensions
    {
        public static IApplicationBuilder UseMaintenanceMode(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MaintenanceModeMiddleware>();
        }
    }
}
