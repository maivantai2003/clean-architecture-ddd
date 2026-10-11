using Auth.Application.Common.Errors;
using MediatR;
namespace Auth.Application.Features.Users.Commands
{
    public record ResetPasswordCommand(string Token,string NewPassword):IRequest<Result<string>>;
}
