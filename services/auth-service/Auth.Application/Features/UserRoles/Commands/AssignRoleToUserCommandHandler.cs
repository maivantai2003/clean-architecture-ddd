using Auth.Application.Abstractions.Persistence;
using MediatR;

namespace Auth.Application.Features.UserRoles.Commands
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, Unit>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AssignRoleToUserCommandHandler(IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork)
        {
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var userRole = new Domain.Entities.UserRole(request.UserId, request.RoleId);
            await _userRoleRepository.AddUserToRoleAsync(userRole);
            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
