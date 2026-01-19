using Auth.Domain.Entities;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleWithPermissionsAsync(Guid roleId);
        Task<Role> GetByIdAsync(Guid roleId);
        Task AddAsync(Role role);
        void Update(Role role);
        void Delete(Role role);
    }
}
