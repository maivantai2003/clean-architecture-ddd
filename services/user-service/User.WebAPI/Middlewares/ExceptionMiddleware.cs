using System.ComponentModel.DataAnnotations;
using System.Net;
using User.Application.Common.Exceptions;

namespace User.WebAPI.Middlewares
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
            catch (ValidationException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (ForbiddenException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.Forbidden,
                    ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled Exception");

                context.Response.StatusCode = 500;

                //await context.Response.WriteAsJsonAsync(
                //    new
                //    {
                //        success = false,
                //        error = "Internal server error",
                //        traceId = context.TraceIdentifier
                //    });
                await HandleException(
                        context,
                        HttpStatusCode.InternalServerError,
                        "Internal server error");
            }
        }
        private static async Task HandleException(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                error = message,
                traceId = context.TraceIdentifier
            });
        }
    }
}
