using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Middleware
{
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppState appState)
        {
            var count = appState.IncrementRequestCount();
            context.Items["RequestNumber"] = count;
            context.Response.Headers["X-Total-Requests"] = count.ToString();

            await _next(context);
        }
    }

    public static class RequestCounterMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCounter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCounterMiddleware>();
        }
    }
}
