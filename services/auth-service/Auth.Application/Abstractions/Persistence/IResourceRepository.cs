using Auth.Domain.Entities;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IResourceRepository
    {
        Task<Resource?> GetByIdAsync(Guid id);
        Task<Resource?> GetByCodeAsync(string code);
        Task<List<Resource>> GetAllAsync();
        Task AddAsync(Resource resource);
        void Remove(Resource resource);
    }
}
