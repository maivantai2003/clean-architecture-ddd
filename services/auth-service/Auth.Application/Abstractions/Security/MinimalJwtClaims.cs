

using Auth.Domain.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Auth.Application.Abstractions.Security
{
    public static class MinimalJwtClaims
    {
        public static IReadOnlyCollection<Claim> Create(User user)
        {
            return new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            //new("perm_ver", user.PermissionVersion.ToString())
        };
        }
    }
}
