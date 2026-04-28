using UserDevice.WebAPI.DTOs;

namespace UserDevice.WebAPI.Middlewares
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
            }catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Fail(ex.Message));
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
