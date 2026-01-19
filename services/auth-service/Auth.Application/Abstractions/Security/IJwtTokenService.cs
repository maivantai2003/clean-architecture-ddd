using System.Security.Claims;

namespace Auth.Application.Abstractions.Security
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
