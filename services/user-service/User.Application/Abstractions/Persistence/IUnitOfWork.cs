namespace User.Application.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveInternalAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
