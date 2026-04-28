using MediatR;
using UserDevice.Application.Abstractions.Persistence;

namespace UserDevice.Application.Features.UserDevices.Commands
{
    public class UpdateUserDeviceCommandHandler : IRequestHandler<UpdateUserDeviceCommand, Guid>
    {
        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserDeviceCommandHandler(IUserDeviceRepository userDeviceRepository, IUnitOfWork unitOfWork)
        {
            _userDeviceRepository = userDeviceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(UpdateUserDeviceCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var device = await _userDeviceRepository.GetByUserDeviceAsync(request.userId, request.deviceId, cancellationToken);
                if (device is null)
                {
                    throw new KeyNotFoundException("User device not found.");
                }
                device.UpdateLoginInfo(request.firebaseToken, request.location);
                await _unitOfWork.CommitInternalAsync(cancellationToken);
                return device.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
