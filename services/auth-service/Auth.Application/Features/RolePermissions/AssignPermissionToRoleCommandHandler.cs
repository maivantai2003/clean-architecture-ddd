using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.RolePermissions
{
    public class AssignPermissionToRoleCommandHandler : IRequestHandler<AssignPermissionToRoleCommand,Unit>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AssignPermissionToRoleCommandHandler(IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;    
            _rolePermissionRepository = rolePermissionRepository;
        }
        public async Task<Unit> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
        {
            var rolePermission = new RolePermission(request.RoleId,request.PermissionId);
            await _rolePermissionRepository.AddPermissionToRoleAsync(rolePermission);
            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
