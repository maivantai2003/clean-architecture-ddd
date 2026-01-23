using Auth.Domain.Common;
using Auth.Domain.Permission.Events;
using System.ComponentModel.DataAnnotations;
namespace Auth.Domain.Entities
{
    public class Permission:AggregateRoot
    {
        [Key]
        public Guid PermissionId { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public Guid ResourceId { get; private set; }
        public Resource Resource { get; private set; } = null!;
        public Guid ActionId { get; private set; }
        public PermissionAction Action { get; private set; } = null!;
        public string ApiEndpoint { get; private set; } = string.Empty;
        public string HttpMethod { get; private set; } = string.Empty;
        public bool IsActive { get; private set; } = true;
        public string Description { get; private set; } = string.Empty;
        public Permission() { }
        public Permission(string code, Guid resourceId, Guid actionId, string apiEndpoint, string httpMethod, bool isActive, string description)
        {
            PermissionId = Guid.NewGuid();
            Code = code;
            ResourceId = resourceId;
            ActionId = actionId;
            ApiEndpoint = apiEndpoint;
            HttpMethod = httpMethod;
            IsActive = isActive;
            Description = description;
        }
        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                AddDomainEvent(new PermissionStatusChangedDomainEvent(PermissionId, false));
            }
        }

        public void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                AddDomainEvent(new PermissionStatusChangedDomainEvent(PermissionId, true));
            }
        }
    }
}
