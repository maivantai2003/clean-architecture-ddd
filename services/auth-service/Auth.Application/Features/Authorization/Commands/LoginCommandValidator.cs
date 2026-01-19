using FluentValidation;

namespace Auth.Application.Features.Authorization.Commands
{
    public class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().Matches(@"^[^@\s]+@gmail\.com$")
                .WithMessage("Email phải có định dạng Gmail hợp lệ.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
