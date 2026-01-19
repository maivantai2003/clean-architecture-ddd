namespace Shared.Contracts.Events
{
    public class UserUpdateEmailIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string NewEmail { get; set; } = null!;
        public UserUpdateEmailIntegrationEvent(Guid userId, string newEmail)
        {
            UserId = userId;
            NewEmail = newEmail;
        }
    }
}
