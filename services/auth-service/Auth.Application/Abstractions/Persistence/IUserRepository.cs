using Auth.Domain.Entities;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
