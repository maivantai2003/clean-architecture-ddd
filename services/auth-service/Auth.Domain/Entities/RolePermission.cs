using System.ComponentModel.DataAnnotations;
namespace Auth.Domain.Entities
{
    public class RolePermission
    {
        [Key]
        public Guid RolePermissionId { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public Role Role { get; private set; } = null!;
        public Permission Permission { get; private set; } = null!;
        public RolePermission() { }

        public RolePermission(Guid roleId, Guid permissionId)
        {
            RolePermissionId = Guid.NewGuid();
            RoleId = roleId;
            PermissionId = permissionId;
        }
        public void UpdatePermission(Guid newPermissionId)
        {
            PermissionId = newPermissionId;
        }
        public void UpdateRole(Guid newRoleId)
        {
            RoleId = newRoleId;
        }
    }
}
