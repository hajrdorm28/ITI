namespace EmployeeManagementSystem.Middleware
{
    public class AuthCheckMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly string[] ProtectedPrefixes =
        {
            "/Employees",
            "/Departments"
        };

        public AuthCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var isProtected = ProtectedPrefixes.Any(p => path.StartsWithSegments(p));

            if (isProtected)
            {
                var username = context.Session.GetString("Username");

                if (string.IsNullOrEmpty(username))
                {
                    var returnUrl = context.Request.Path + context.Request.QueryString;
                    context.Response.Redirect($"/Account/Login?returnUrl={Uri.EscapeDataString(returnUrl)}");
                    return; // short-circuit
                }
            }

            await _next(context);
        }
    }

    public static class AuthCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthCheck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthCheckMiddleware>();
        }
    }
}
