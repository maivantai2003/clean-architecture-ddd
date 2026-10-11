namespace Shared.Contracts.Events
{
    public class PasswordResetRequestedIntegrationEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string ResetToken { get; }
        public DateTime ExpiresAtUtc { get; }
        public PasswordResetRequestedIntegrationEvent(Guid userId, string email, string resetToken, DateTime expiresAtUtc)
        {
            UserId = userId;
            Email = email;
            ResetToken = resetToken;
            ExpiresAtUtc = expiresAtUtc;
        }
    }
}
