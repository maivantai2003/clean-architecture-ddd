using Auth.Application.Abstractions.Persistence;
using MediatR;

namespace Auth.Application.Features.Resources.Commands
{
    public class UpdateResourceHandler : IRequestHandler<UpdateResourceCommand, Unit>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateResourceHandler(IResourceRepository resourceRepository, IUnitOfWork unitOfWork)
        {
            _resourceRepository = resourceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(request.ResourceId)
            ?? throw new Exception("Resource not found");

            resource.Update(request.Name, request.Description);

            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
