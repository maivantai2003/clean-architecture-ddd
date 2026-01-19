using Auth.Application.Abstractions.Persistence;
using MediatR;

namespace Auth.Application.Features.Resources.Commands
{
    public class DeleteResourceHandler : IRequestHandler<DeleteResourceCommand,Unit>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteResourceHandler(IResourceRepository resourceRepository,IUnitOfWork unitOfWork)
        {
            _resourceRepository = resourceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(request.ResourceId)
            ?? throw new Exception("Resource not found");
            _resourceRepository.Remove(resource);
            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
