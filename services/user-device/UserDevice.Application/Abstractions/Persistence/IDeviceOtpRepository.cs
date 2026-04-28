using UserDevice.Domain.Entities;

namespace UserDevice.Application.Abstractions.Persistence
{
    public interface IDeviceOtpRepository
    {
        Task AddAsync(DeviceOtp otp, CancellationToken cancellationToken);
        Task<DeviceOtp?> GetLatestValidOtpAsync(Guid userDeviceId, CancellationToken cancellationToken);
    }
}
