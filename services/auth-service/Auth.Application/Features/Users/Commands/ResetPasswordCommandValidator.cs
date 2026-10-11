using FluentValidation;

namespace Auth.Application.Features.Users.Commands
{
    public class ResetPasswordCommandValidator:AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.")
                .MaximumLength(200).WithMessage("Token must not exceed 200 characters.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("New password must not exceed 100 characters.");
        }
    }
}
