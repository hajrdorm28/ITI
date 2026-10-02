namespace EmployeeManagementSystem.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(
                $"[RequestLogger] BEFORE next() -> {context.Request.Method} {context.Request.Path} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            await _next(context);

            Console.WriteLine(
                $"[RequestLogger] AFTER next()  -> {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode}");
        }
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
