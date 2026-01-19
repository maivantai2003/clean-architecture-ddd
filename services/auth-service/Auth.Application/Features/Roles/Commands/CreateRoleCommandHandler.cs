using Auth.Application.Abstractions.Persistence;
using MediatR;

namespace Auth.Application.Features.Roles.Commands
{
    public class CreateRoleCommandHandler:IRequestHandler<CreateRoleCommand,Guid>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRoleCommandHandler(IRoleRepository roleRepository,IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Domain.Entities.Role(request.roleCode, request.roleName, request.description);
            await _roleRepository.AddAsync(role);
            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return role.RoleId;
        }   
    }
}
