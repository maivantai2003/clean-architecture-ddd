using Auth.Application.Common.Errors;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public record ForgotPasswordCommand(string Email) : IRequest<Result<string>>;
}
