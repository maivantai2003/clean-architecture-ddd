using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class RolePermissionRepository:IRolePermissionRepository
    {
        private readonly AuthDbContext _context;
        public RolePermissionRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RolePermission>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            return await _context.RolePermissions.Where(rp => rp.RoleId == roleId).ToListAsync();
        }

        public async Task AddPermissionToRoleAsync(RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
        }

        public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var record = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            if (record != null) _context.RolePermissions.Remove(record);
        }

        public async Task<bool> IsPermissionInRoleAsync(Guid roleId, Guid permissionId)
        {
            return await _context.RolePermissions.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }
    }
}
