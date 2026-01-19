using Auth.Domain.Common;

namespace Auth.Domain.Users.Events
{
    public class UserCreatedDomainEvent:DomainEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Name { get; }
        public UserCreatedDomainEvent(Guid userId,string email,string name)
        {
            UserId = userId;
            Email = email;
            Name = name;

        }
    }
}
