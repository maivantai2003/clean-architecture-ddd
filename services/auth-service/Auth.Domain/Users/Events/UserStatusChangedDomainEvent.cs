using Auth.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Users.Events
{
    public class UserStatusChangedDomainEvent : DomainEvent
    {
        public Guid UserId { get; }
        public bool IsActive { get; }

        public UserStatusChangedDomainEvent(Guid userId, bool isActive)
        {
            UserId = userId;
            IsActive = isActive;
        }
    }
}
