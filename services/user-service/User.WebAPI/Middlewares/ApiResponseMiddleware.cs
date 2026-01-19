using User.Application.Common.Exceptions;
using User.WebAPI.DTOs;

namespace User.WebAPI.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                Console.WriteLine(">>> ApiResponseMiddleware START");
                await _next(context);
                Console.WriteLine(">>> ApiResponseMiddleware END");
            }
            catch (UnauthorizedException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Fail(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Fail(ex.Message));
            }
            catch (ForbiddenException ex)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Fail(ex.Message));
            }
            catch (AuthorizationServiceUnavailableException ex)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Fail("Authorization service unavailable"));
            }
            catch (InvalidPermissionResponseException ex)
            {
                context.Response.StatusCode = StatusCodes.Status502BadGateway;
                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Fail("Invalid permission response from auth service"));
            }
            catch (Exception ex)
            {
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //await context.Response.WriteAsJsonAsync(
                //    ApiResponse<object>.Fail("Internal server error"));
                Console.WriteLine(">>> ApiResponseMiddleware CATCH: " + ex.GetType().FullName);
                throw;
            }
        }
    }
}
