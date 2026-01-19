using MediatR;

namespace Auth.Application.Features.Permissions.Commands
{
    public record CreatePermissionCommand(string Code, Guid ResourceId, Guid ActionId, string ApiEndpoint, string HttpMethod,string Description):IRequest<Guid>;
}
