using Auth.Application.Common.Errors;
using MediatR;
namespace Auth.Application.Features.Users.Commands
{
    public record CreateUserCommand(string userName,string Email,string Password):IRequest<Result<Guid>>;
}
