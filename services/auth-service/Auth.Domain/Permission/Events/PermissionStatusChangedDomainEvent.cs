using Auth.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Permission.Events
{
    public class PermissionStatusChangedDomainEvent : DomainEvent
    {
        public Guid PermissionId { get; }
        public bool IsActive { get; }

        public PermissionStatusChangedDomainEvent(Guid permissionId, bool isActive)
        {
            PermissionId = permissionId;
            IsActive = isActive;
        }
    }
}
