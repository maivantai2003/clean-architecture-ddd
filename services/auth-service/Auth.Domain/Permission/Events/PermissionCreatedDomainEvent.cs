using Auth.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Permission.Events
{
    public class PermissionCreatedDomainEvent : DomainEvent
    {
        public Guid PermissionId { get; }

        public PermissionCreatedDomainEvent(Guid permissionId)
        {
            PermissionId = permissionId;
        }
    }
}
