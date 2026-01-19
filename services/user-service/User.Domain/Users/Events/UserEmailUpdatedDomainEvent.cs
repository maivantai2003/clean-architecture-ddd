using User.Domain.Common;

namespace User.Domain.Users.Events
{
    public class UserEmailUpdatedDomainEvent:DomainEvent
    {
        public Guid UserId { get; }
        public string NewEmail { get; }

        public UserEmailUpdatedDomainEvent(Guid userId, string newEmail)
        {
            UserId = userId;
            NewEmail = newEmail;
        }
    }
}
