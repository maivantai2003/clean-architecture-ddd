using Auth.Application.Abstractions.Persistence;
using MediatR;

namespace Auth.Application.Features.Actions.Commands
{
    public class UpdateActionCommandHandler:IRequestHandler<UpdateActionCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActionRepository _actionRepository;
        public UpdateActionCommandHandler(IUnitOfWork unitOfWork,IActionRepository actionRepository)
        {
            _unitOfWork = unitOfWork;
            _actionRepository = actionRepository;
        }
        public async Task<Unit> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
        {
            var action = await _actionRepository.GetByIdAsync(request.ActionId)
                ?? throw new Exception("Action not found");
            action.Update(request.Name, request.Description);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
