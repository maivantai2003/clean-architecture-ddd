namespace Auth.Application.Abstractions.Security
{
    public interface ITokenLifetimeProvider
    {
        int AccessTokenMinutes { get; }
        int RefreshTokenDays { get; }
    }

}
