using Auth.Domain.Common;
using Auth.Domain.Roles.Events;
using System.ComponentModel.DataAnnotations;
namespace Auth.Domain.Entities
{
    public class Role:AggregateRoot
    {
        [Key]
        public Guid RoleId { get; private set; }
        public string RoleCode { get; private set; }
        public string RoleName { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        private readonly List<RolePermission> _permissions = new();
        public IReadOnlyCollection<RolePermission> Permissions => _permissions.AsReadOnly();
        public Role() { }
        public Role(string roleCode, string roleName, string description)
        {
            RoleId = Guid.NewGuid();
            RoleCode = roleCode;
            RoleName = roleName;
            Description = description;
        }
        public void ReName(string newRoleName)
        {
            RoleName = newRoleName;
        } 
        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }
        public void ChangeRoleCode(string newRoleCode)
        {
            RoleCode = newRoleCode;
        }
        public void ChangePermissions()
        {
            AddDomainEvent(new RolePermissionChangedDomainEvent(RoleId));
        }
        public void AddPermission(Guid permissionId)
        {
            if (_permissions.Any(p => p.PermissionId == permissionId))
                return;

            _permissions.Add(new RolePermission(RoleId, permissionId));
            AddDomainEvent(new RolePermissionChangedDomainEvent(RoleId));
        }
        public void RemovePermission(Guid permissionId)
        {
            var p = _permissions.FirstOrDefault(x => x.PermissionId == permissionId);
            if (p == null) return;

            _permissions.Remove(p);
            AddDomainEvent(new RolePermissionChangedDomainEvent(RoleId));
        }
    }
}
