using MediatR;
namespace Auth.Application.Features.RolePermissions
{
    public record AssignPermissionToRoleCommand(Guid RoleId, Guid PermissionId): IRequest<Unit>;
}
