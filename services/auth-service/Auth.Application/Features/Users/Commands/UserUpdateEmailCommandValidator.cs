using FluentValidation;

namespace Auth.Application.Features.Users.Commands
{
    public class UserUpdateEmailCommandValidator : AbstractValidator<UserUpdateEmailCommand>
    {
        public UserUpdateEmailCommandValidator()
        {
            RuleFor(x => x.NewEmail).NotEmpty().Matches(@"^[^@\s]+@gmail\.com$")
                .WithMessage("Email phải có định dạng Gmail hợp lệ.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId không được để trống.");
        }
    }
}
