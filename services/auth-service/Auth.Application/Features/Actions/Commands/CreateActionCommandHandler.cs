using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Actions.Commands
{
    public class CreateActionCommandHandler : IRequestHandler<CreateActionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppActionRepository actionRepository;
        public CreateActionCommandHandler(IUnitOfWork unitOfWork, IAppActionRepository actionRepository)
        {
            _unitOfWork = unitOfWork;
            this.actionRepository = actionRepository;
        }

        public async Task<Guid> Handle(CreateActionCommand request, CancellationToken cancellationToken)
        {
            var action = new PermissionAction
            {
                ActionCode = request.Code,
                ActionName = request.Name,
                Description = request.Description
            };
            await actionRepository.AddAsync(action);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return action.ActionId;
        }
    }
}
