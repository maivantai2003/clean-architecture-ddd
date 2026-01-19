namespace Shared.Contracts.Events
{
    public class UserRegisteredIntegrationEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty; 
        public UserRegisteredIntegrationEvent(Guid userId, string username, string email) 
        { UserId = userId; Username = username; Email = email; }

    }
}