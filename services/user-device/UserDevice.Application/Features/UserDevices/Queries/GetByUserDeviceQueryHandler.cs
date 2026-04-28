using MediatR;
using UserDevice.Application.Abstractions.Persistence;
using UserDevice.Application.DTOs;

namespace UserDevice.Application.Features.UserDevices.Queries
{
    public class GetByUserDeviceQueryHandler : IRequestHandler<GetByUserDeviceQuery, UserDeviceDTO?>
    {
        private readonly IUserDeviceRepository _userDeviceRepository;
        public GetByUserDeviceQueryHandler(IUserDeviceRepository userDeviceRepository)
        {
            _userDeviceRepository = userDeviceRepository;
        }
        public async Task<UserDeviceDTO> Handle(GetByUserDeviceQuery request, CancellationToken cancellationToken)
        {
            var result = await _userDeviceRepository.GetByUserDeviceAsync(request.userId, request.deviceId, cancellationToken);
            if (result is null)
            {
                throw new KeyNotFoundException("User device not found.");
            }
            return new UserDeviceDTO
            {
                Id = result.Id,
                UserId = result.UserId,
                Location = result.Location,
                DeviceId = result.DeviceId,
                DeviceType = result.DeviceType,
                FirebaseToken = result.FirebaseToken
            };
        }
    }
}
