using System.Security.Claims;

namespace User.Application.Authorization
{
    public interface IAuthorizationService
    {
        Task EnsurePermissionAsync(ClaimsPrincipal user,Permission permission,CancellationToken ct = default);
    }
}
