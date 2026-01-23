using Auth.Domain.Entities;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IAppActionRepository
    {
        Task<PermissionAction?> GetByIdAsync(Guid id);
        Task<PermissionAction?> GetByCodeAsync(string code);
        Task<List<PermissionAction>> GetAllAsync();
        Task AddAsync(PermissionAction action);
        void Remove(PermissionAction action);
    }
}
