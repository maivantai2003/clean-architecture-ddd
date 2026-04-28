using Auth.Application.DTOs;
using MediatR;
namespace Auth.Application.Features.Users.Commands
{
    public record CreateUserCommand(string userName,string Email,string Password):IRequest<Result<Guid>>;
}
