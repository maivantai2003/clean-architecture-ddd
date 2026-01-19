using Auth.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Users.Events
{
    public class UserRoleAssignedDomainEvent:DomainEvent
    {
        public Guid UserId { get; }
        public Guid RoleId { get; }
        public UserRoleAssignedDomainEvent(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
