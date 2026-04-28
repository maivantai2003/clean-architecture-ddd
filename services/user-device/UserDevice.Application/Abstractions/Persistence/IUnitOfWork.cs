namespace UserDevice.Application.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveInternalAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitInternalAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
