using MediatR;
namespace Auth.Application.Features.Permissions.Queries
{
    public record CheckPermissionQuery(Guid UserId, string Permission) : IRequest<bool>;
}
