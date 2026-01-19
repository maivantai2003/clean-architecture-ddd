using Auth.Domain.Entities;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetRolesByUserIdAsync(Guid userId);
        Task AddUserToRoleAsync(UserRole userRole);
        Task RemoveUserFromRoleAsync(Guid userId, Guid roleId);
        Task<bool> IsUserInRoleAsync(Guid userId, Guid roleId);
    }
}
