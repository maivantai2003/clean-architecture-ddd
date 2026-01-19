using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public record AssignRoleToUserCommand(Guid UserId, Guid RoleId) : IRequest<Unit>;
}
