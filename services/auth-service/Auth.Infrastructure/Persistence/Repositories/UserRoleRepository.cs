using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class UserRoleRepository:IUserRoleRepository
    {
        private readonly AuthDbContext _context;
        public UserRoleRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserRole>> GetRolesByUserIdAsync(Guid userId)
        {
            return await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
        }

        public async Task AddUserToRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public async Task RemoveUserFromRoleAsync(Guid userId, Guid roleId)
        {
            var record = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (record != null) _context.UserRoles.Remove(record);
        }

        public async Task<bool> IsUserInRoleAsync(Guid userId, Guid roleId)
        {
            return await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
