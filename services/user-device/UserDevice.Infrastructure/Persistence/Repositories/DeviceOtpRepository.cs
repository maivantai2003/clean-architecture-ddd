using Microsoft.EntityFrameworkCore;
using UserDevice.Application.Abstractions.Persistence;
using UserDevice.Domain.Entities;

namespace UserDevice.Infrastructure.Persistence.Repositories
{
    internal class DeviceOtpRepository : IDeviceOtpRepository
    {
        private readonly UserDeviceDbContext _context;

        public DeviceOtpRepository(UserDeviceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DeviceOtp otp, CancellationToken cancellationToken)
        {
            await _context.DeviceOtps.AddAsync(otp, cancellationToken);
        }

        public async Task<DeviceOtp?> GetLatestValidOtpAsync(
            Guid userDeviceId,
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            return await _context.DeviceOtps.Where(x =>x.UserDeviceId == userDeviceId &&!x.IsUsed &&x.ExpiredAt >= now).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
