using MediatR;

namespace Auth.Application.Features.UserRoles.Commands
{
    public record AssignRoleToUserCommand(Guid UserId,Guid RoleId) : IRequest<Unit>;
}
