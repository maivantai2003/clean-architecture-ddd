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
            
        }
    }
}
