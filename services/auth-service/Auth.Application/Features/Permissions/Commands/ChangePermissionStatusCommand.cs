using MediatR;

namespace Auth.Application.Features.Permissions.Commands
{
    public record ChangePermissionStatusCommand(Guid PermissionId, bool IsActive):IRequest<Unit>;
}
