using Auth.Application.Abstractions.Persistence;
using Microsoft.AspNetCore.Http;
namespace Auth.Infrastructure.Persistence.Identity
{
    public class CurrentUser:ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId =>Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("sub")!.Value);
    }
}
