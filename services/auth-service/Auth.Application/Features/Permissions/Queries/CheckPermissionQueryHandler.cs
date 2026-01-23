using Auth.Application.Abstractions.Persistence;
using MediatR;

namespace Auth.Application.Features.Permissions.Queries
{
    public class CheckPermissionQueryHandler : IRequestHandler<CheckPermissionQuery, bool>
    {
        private readonly IPermissionChecker _permissionChecker;
        public CheckPermissionQueryHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }
        public async Task<bool> Handle(CheckPermissionQuery request, CancellationToken cancellationToken)
        {
            return await _permissionChecker.HasPermissionAsync(request.UserId, request.Permission, cancellationToken);
        }
    }
}
