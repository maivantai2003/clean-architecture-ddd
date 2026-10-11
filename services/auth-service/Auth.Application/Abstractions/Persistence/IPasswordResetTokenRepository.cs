using Auth.Domain.Entities;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IPasswordResetTokenRepository
    {
        Task AddAsync(
        PasswordResetToken token,
        CancellationToken cancellationToken);

        Task<PasswordResetToken?> GetByTokenHashAsync(
            string tokenHash,
            CancellationToken cancellationToken);

        Task RevokeActiveTokensAsync(
            Guid userId,
            DateTime utcNow,
            CancellationToken cancellationToken);
    }
}
