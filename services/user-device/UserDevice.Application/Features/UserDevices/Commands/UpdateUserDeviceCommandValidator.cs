using FluentValidation;

namespace UserDevice.Application.Features.UserDevices.Commands
{
    public class UpdateUserDeviceCommandValidator: AbstractValidator<UpdateUserDeviceCommand>
    {
        public UpdateUserDeviceCommandValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.deviceId)
                .NotEmpty().WithMessage("DeviceId is required.")
                .MaximumLength(100).WithMessage("DeviceId cannot exceed 100 characters.");
            RuleFor(x => x.location)
                .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");
            RuleFor(x => x.firebaseToken)
                .MaximumLength(500).WithMessage("DeviceToken cannot exceed 500 characters.");
        }
    }
}
