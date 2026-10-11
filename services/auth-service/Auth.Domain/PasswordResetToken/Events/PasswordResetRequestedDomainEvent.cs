using Auth.Domain.Common;

namespace Auth.Domain.PasswordResetToken.Events
{
    public class PasswordResetRequestedDomainEvent: DomainEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string ResetToken { get; }
        public DateTime ExpiresAtUtc { get; }
        public PasswordResetRequestedDomainEvent(Guid userId, string email, string resetToken, DateTime expiresAtUtc)
        {
            UserId = userId;
            Email = email;
            ResetToken = resetToken;
            ExpiresAtUtc = expiresAtUtc;
        }
    }
}
