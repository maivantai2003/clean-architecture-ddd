using Auth.Domain.Entities;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IPermissionRepository
    {
        Task<Permission?> GetByIdAsync(Guid permissionId);
        Task<IEnumerable<Permission>> GetAllAsync();
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(Guid permissionId);
    }
}
