using Auth.Domain.Entities;
namespace Auth.Application.Abstractions.Security
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByHashAsync(string tokenHash);
        Task AddAsync(RefreshToken token);
        Task RevokeAsync(RefreshToken token);
        Task RevokeAllByUserAsync(Guid userId);
    }
}
