using Auth.Application.Abstractions.Security;
using Microsoft.Extensions.Options;
namespace Auth.Infrastructure.Security
{
    public sealed class TokenLifetimeProvider : ITokenLifetimeProvider
    {
        public int AccessTokenMinutes { get; }
        public int RefreshTokenDays { get; }

        public TokenLifetimeProvider(IOptions<JwtOptions> options)
        {
            AccessTokenMinutes = options.Value.AccessTokenMinutes;
            RefreshTokenDays = options.Value.RefreshTokenDays;
        }
    }
}
