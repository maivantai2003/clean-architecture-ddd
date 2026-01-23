namespace Realtime.Application.Interfaces
{
    public interface IUserConnectionStore
    {
        Task AddAsync(Guid userId, string connectionId);
        Task RemoveAsync(Guid userId, string connectionId);
        Task<IReadOnlyList<string>> GetConnectionsAsync(Guid userId);
    }
}
