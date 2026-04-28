using FluentValidation;

namespace UserDevice.Application.Features.DeviceOtps.Commands
{
    public class VerifyDeviceOtpCommandValidator:AbstractValidator<VerifyDeviceOtpCommand>
    {
        public VerifyDeviceOtpCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage("DeviceId is required.")
                .MaximumLength(100).WithMessage("DeviceId cannot exceed 100 characters.");
            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("OtpCode is required.")
                .MaximumLength(7).WithMessage("OtpCode cannot exceed 10 characters.");
        }
    }
}
