using Auth.Application.Abstractions.Persistence;
using MediatR;

namespace Auth.Application.Features.Permissions.Commands
{
    public class ChangePermissionStatusCommandHandler : IRequestHandler<ChangePermissionStatusCommand, Unit>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChangePermissionStatusCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ChangePermissionStatusCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.PermissionId);
            if (permission == null)
            {
                throw new KeyNotFoundException($"Permission with id {request.PermissionId} not found.");
            }
            if (request.IsActive)
            {
                permission.Activate();
            }
            else
            {
                permission.Deactivate();
            }
            await _unitOfWork.SaveInternalAsync();
            return Unit.Value;
        }
    }
}
