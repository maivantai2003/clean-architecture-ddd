namespace UserDevice.Application.Abstractions.Persistence
{
    public interface IUserDeviceRepository
    {
        Task<bool> ExistsAsync(Guid userId, string deviceId, CancellationToken cancellationToken);
        Task AddAsync(Domain.Entities.UserDevice userDevice, CancellationToken cancellationToken);
        Task<Domain.Entities.UserDevice?> GetByUserDeviceAsync(Guid userId, string deviceId, CancellationToken cancellationToken);
    }
}
