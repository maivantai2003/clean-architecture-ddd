using User.Domain.Entities;

namespace User.Application.Abstractions.Persistence
{
    public interface IUserRepository
    {
        Task<Domain.Entities.User?> GetByIdAsync(Guid userId);
        Task<List<Domain.Entities.User>> GetAllAsync();
        Task<Domain.Entities.User?> GetByEmailAsync(string email);
        Task<Domain.Entities.User?> GetByUsernameAsync(string username);

        Task<bool> ExistsAsync(Guid userId);

        Task AddAsync(Domain.Entities.User user);
        void Update(Domain.Entities.User user);
    }
}
