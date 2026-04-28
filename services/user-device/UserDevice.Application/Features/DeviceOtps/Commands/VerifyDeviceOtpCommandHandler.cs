using MediatR;
using UserDevice.Application.Abstractions.Persistence;

namespace UserDevice.Application.Features.DeviceOtps.Commands
{
    public class VerifyDeviceOtpCommandHandler : IRequestHandler<VerifyDeviceOtpCommand, Unit>
    {
        private readonly IUserDeviceRepository _deviceRepository;
        private readonly IDeviceOtpRepository _otpRepository;
        private readonly IUnitOfWork _unitOfWork;
        public VerifyDeviceOtpCommandHandler(IUserDeviceRepository deviceRepository,IDeviceOtpRepository otpRepository,
            IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository;
            _otpRepository = otpRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(VerifyDeviceOtpCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByUserDeviceAsync(request.UserId, request.DeviceId, cancellationToken)?? throw new KeyNotFoundException("Device not found");
            var otp = await _otpRepository.GetLatestValidOtpAsync(device.Id, cancellationToken)
            ?? throw new InvalidOperationException("OTP not found");
            otp.Validate(request.OtpCode);
            otp.MarkUsed();

            device.MarkTrusted();
            device.UpdateLastLogin();

            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
