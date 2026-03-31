using System.Net;
using System.Text.Json;

namespace BudgetTracker.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                ArgumentException => HttpStatusCode.BadRequest,      // 400
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
                KeyNotFoundException => HttpStatusCode.NotFound,     // 404
                InvalidOperationException => HttpStatusCode.Conflict, // 409
                _ => HttpStatusCode.InternalServerError              // 500
            };

            context.Response.StatusCode = (int)statusCode;

            var message = statusCode == HttpStatusCode.InternalServerError
                ? "An unexpected error occurred"
                : ex.Message;

            var response = new
            {
                status = context.Response.StatusCode,
                message
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
