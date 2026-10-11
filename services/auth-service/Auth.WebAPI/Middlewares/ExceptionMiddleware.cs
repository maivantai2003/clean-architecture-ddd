using Auth.WebAPI.DTOs;

namespace Auth.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        success = false,
                        error = ex.Message,
                        traceId = context.TraceIdentifier
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled Exception");

                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        success = false,
                        error = "Internal server error",
                        traceId = context.TraceIdentifier
                    });
            }
        }
        //private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        //{
        //    context.Response.ContentType =
        //        "application/json";

        //    context.Response.StatusCode = 500;

        //    var response = new ApiResponse<object>
        //    {
        //        Success = false,
        //        Error = new Error("System.InternalError",
        //            "An unexpected error occurred."),
        //        TraceId = context.TraceIdentifier
        //    };

        //    await context.Response.WriteAsJsonAsync(response);
        //}
    }
}
