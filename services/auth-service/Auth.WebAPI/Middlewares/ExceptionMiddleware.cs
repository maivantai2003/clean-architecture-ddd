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
    }
}
