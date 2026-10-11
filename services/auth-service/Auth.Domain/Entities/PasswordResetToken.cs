using Auth.Domain.Common;
using Auth.Domain.PasswordResetToken.Events;

namespace Auth.Domain.Entities
{
    public class PasswordResetToken:AggregateRoot
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public string TokenHash { get; private set; } = null!;

        public DateTime ExpiresAtUtc { get; private set; }

        public DateTime? UsedAtUtc { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        public bool IsUsed => UsedAtUtc.HasValue;

        public bool IsExpired(DateTime utcNow)
            => utcNow >= ExpiresAtUtc;

        private PasswordResetToken()
        {
        }

        private PasswordResetToken(Guid id,Guid userId,string tokenHash,DateTime createdAtUtc,
            DateTime expiresAtUtc)
        {
            Id = id;
            UserId = userId;
            TokenHash = tokenHash;
            CreatedAtUtc = createdAtUtc;
            ExpiresAtUtc = expiresAtUtc;
        }

        public static PasswordResetToken Create(Guid userId,string tokenHash,DateTime createdAtUtc,
            DateTime expiresAtUtc)
        {
            //if (userId == Guid.Empty)
            //    throw new DomainException(
            //        "UserId cannot be empty.");

            //if (string.IsNullOrWhiteSpace(tokenHash))
            //    throw new DomainException(
            //        "Token hash cannot be empty.");

            //if (expiresAtUtc <= createdAtUtc)
            //    throw new DomainException(
            //        "Token expiration must be greater than creation time.");
            return new PasswordResetToken(Guid.NewGuid(),userId,tokenHash,createdAtUtc,
                expiresAtUtc);
        }

        public void MarkAsUsed(DateTime utcNow)
        {
            //if (IsUsed)
            //    throw new DomainException(
            //        "Reset token has already been used.");

            //if (IsExpired(utcNow))
            //    throw new DomainException(
            //        "Reset token has expired.");

            UsedAtUtc = utcNow;
        }
    }
}
