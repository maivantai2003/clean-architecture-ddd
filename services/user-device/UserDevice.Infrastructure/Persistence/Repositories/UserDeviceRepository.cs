using Microsoft.EntityFrameworkCore;
using UserDevice.Application.Abstractions.Persistence;

namespace UserDevice.Infrastructure.Persistence.Repositories
{
    public class UserDeviceRepository : IUserDeviceRepository
    {
        private readonly UserDeviceDbContext _dbContext;
        public UserDeviceRepository(UserDeviceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Domain.Entities.UserDevice userDevice, CancellationToken cancellationToken)
        {
            await _dbContext.userDevices.AddAsync(userDevice, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid userId, string deviceId, CancellationToken cancellationToken)
        {
            var result =await _dbContext.userDevices.FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DeviceId == deviceId);
            return result is not null;
        }

        public async Task<Domain.Entities.UserDevice?> GetByUserDeviceAsync(Guid userId, string deviceId, CancellationToken cancellationToken)
        {
            var result = await _dbContext.userDevices.FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DeviceId == deviceId, cancellationToken);
            return result;
        }
    }
}
