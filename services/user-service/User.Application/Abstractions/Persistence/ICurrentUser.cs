using System.Security.Claims;

namespace User.Application.Abstractions.Persistence
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        Guid UserId { get; }
        ClaimsPrincipal Principal { get; }
    }
}
