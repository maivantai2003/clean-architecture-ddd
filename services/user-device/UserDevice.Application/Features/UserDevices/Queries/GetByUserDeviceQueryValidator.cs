using FluentValidation;

namespace UserDevice.Application.Features.UserDevices.Queries
{
    public class GetByUserDeviceQueryValidator:AbstractValidator<GetByUserDeviceQuery>
    {
        public GetByUserDeviceQueryValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.deviceId)
                .NotEmpty().WithMessage("DeviceId is required.")
                .MaximumLength(100).WithMessage("DeviceId cannot exceed 100 characters.");
        }
    }
}
