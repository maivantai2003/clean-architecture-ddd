using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class PasswordResetTokenRepository(AuthDbContext _dbContext) : IPasswordResetTokenRepository
    {
        public async Task AddAsync(PasswordResetToken token, CancellationToken cancellationToken)
        {
            await _dbContext.PasswordResetTokens.AddAsync(token, cancellationToken);
        }

        public async Task<PasswordResetToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken)
        {
            return await _dbContext.PasswordResetTokens.FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);
        }

        public async Task RevokeActiveTokensAsync(Guid userId, DateTime utcNow, CancellationToken cancellationToken)
        {
            var tokens = await _dbContext.PasswordResetTokens.Where(x =>x.UserId == userId && x.UsedAtUtc == null &&
                x.ExpiresAtUtc > utcNow).ToListAsync(cancellationToken);

            foreach (var token in tokens)
            {
                token.MarkAsUsed(utcNow);
            }
        }
    }
}
