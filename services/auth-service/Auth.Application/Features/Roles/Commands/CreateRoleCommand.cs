using MediatR;

namespace Auth.Application.Features.Roles.Commands
{
    public record CreateRoleCommand(string roleCode,string roleName, string description):IRequest<Guid>;
}
