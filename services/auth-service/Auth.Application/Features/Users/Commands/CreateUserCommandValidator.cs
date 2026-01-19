using FluentValidation;

namespace Auth.Application.Features.Users.Commands
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.userName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Username phải có ít nhất 3 ký tự.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(@"^[^@\s]+@gmail\.com$")
                .WithMessage("Email phải có định dạng Gmail hợp lệ.");
            RuleFor(x => x.PasswordHash)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Mật khẩu phải có ít nhất 6 ký tự.");
        }
    }
}
