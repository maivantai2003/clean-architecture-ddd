using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Permissions.Commands
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionRepository _permissionRepository;
        public CreatePermissionCommandHandler(IUnitOfWork unitOfWork, IPermissionRepository permissionRepository)
        {
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
        }
        public async Task<Guid> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Domain.Entities.Permission(request.Code,request.ResourceId,
            request.ActionId,request.ApiEndpoint,request.HttpMethod,true,request.Description);
            await _permissionRepository.AddAsync(permission);
            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return permission.PermissionId;
        }
    }
}
