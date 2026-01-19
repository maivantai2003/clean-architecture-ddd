using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class PermissionsRepository : IPermissionRepository
    {
        private readonly AuthDbContext _context;
        public PermissionsRepository(AuthDbContext context) {
            _context = context; 
        } 
        public async Task<Permission?> GetByIdAsync(Guid permissionId) { 
            return await _context.Permissions.FirstOrDefaultAsync(p => p.PermissionId == permissionId); 
        } 
        public async Task<IEnumerable<Permission>> GetAllAsync() {
            return await _context.Permissions.ToListAsync(); 
        } 
        public async Task AddAsync(Permission permission) { 
            await _context.Permissions.AddAsync(permission);
        } 
        public async Task UpdateAsync(Permission permission) {
            _context.Permissions.Update(permission);
        } 
        public async Task DeleteAsync(Guid permissionId) { 
            var permission = await GetByIdAsync(permissionId); 
            if (permission != null) {
                _context.Permissions.Remove(permission); 
            } 
        } 
    }
}
