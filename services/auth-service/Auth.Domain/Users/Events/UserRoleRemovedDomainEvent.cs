
using Auth.Domain.Common;

namespace Auth.Domain.Users.Events
{
    public class UserRoleRemovedDomainEvent : DomainEvent
    {
        public Guid UserId { get; }
        public Guid RoleId { get; }

        public UserRoleRemovedDomainEvent(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
