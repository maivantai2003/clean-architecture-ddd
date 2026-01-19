namespace Auth.Application.Abstractions.Persistence
{
    public interface IActionRepository
    {
        Task<Domain.Entities.Action?> GetByIdAsync(Guid id);
        Task<Domain.Entities.Action?> GetByCodeAsync(string code);
        Task<List<Domain.Entities.Action>> GetAllAsync();
        Task AddAsync(Domain.Entities.Action action);
        void Remove(Domain.Entities.Action action);
    }
}
