using Auth.Domain.Entities;
namespace Auth.Application.Abstractions.Persistence
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<RolePermission>> GetPermissionsByRoleIdAsync(Guid roleId);
        Task AddPermissionToRoleAsync(RolePermission rolePermission);
        Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId);
        Task<bool> IsPermissionInRoleAsync(Guid roleId, Guid permissionId);
    }
}
