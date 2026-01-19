using System.Security.Claims;
using User.Application.Authorization;
using User.Application.Common.Exceptions;
using User.Application.Security;

namespace User.Infrastructure.Security
{
    public class AuthorizationService: IAuthorizationService
    {
        private readonly IPermissionChecker _checker;

        public AuthorizationService(
            IPermissionChecker checker)
        {
            _checker = checker;
        }

        public async Task EnsurePermissionAsync(ClaimsPrincipal user,Permission permission,CancellationToken ct)
        {

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                throw new UnauthorizedException("UserId claim missing");

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                throw new UnauthorizedException("Invalid UserId");

            var allowed = await _checker.HasPermissionAsync(
                userId,
                permission.ToString(),
                ct);

            if (!allowed)
                throw new ForbiddenException(permission.ToString());
        }
    }
}
