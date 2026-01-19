using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using User.Application.Abstractions.Persistence;
using User.Application.Common.Exceptions;

namespace User.Infrastructure.Security
{
    public sealed class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUser(IHttpContextAccessor http)
        {
            _http = http;
        }

        public ClaimsPrincipal Principal =>
            _http.HttpContext?.User
            ?? throw new UnauthorizedException("User not authenticated");

        public bool IsAuthenticated =>
            Principal.Identity?.IsAuthenticated ?? false;

        public Guid UserId => Guid.Parse(Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedException("UserId missing"));
    }

}
