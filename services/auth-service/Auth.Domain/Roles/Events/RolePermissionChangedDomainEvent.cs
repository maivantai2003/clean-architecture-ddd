using Auth.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Roles.Events
{
    public class RolePermissionChangedDomainEvent : DomainEvent
    {
        public Guid RoleId { get; }
        public RolePermissionChangedDomainEvent(Guid roleId)
        {
            RoleId = roleId;
        }
    }
}
