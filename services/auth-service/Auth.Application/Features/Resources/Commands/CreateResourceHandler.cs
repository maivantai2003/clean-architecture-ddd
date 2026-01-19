using Auth.Application.Abstractions.Persistence;
using MediatR;
namespace Auth.Application.Features.Resources.Commands
{
    public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, Guid>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateResourceHandler(IResourceRepository resourceRepository, IUnitOfWork unitOfWork)
        {
            _resourceRepository = resourceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            if (await _resourceRepository.GetByCodeAsync(request.Code) != null)
                throw new Exception("Resource code already exists");
            var resource = new Domain.Entities.Resource(
            request.Code,
            request.Name,
            request.Description);

            await _resourceRepository.AddAsync(resource);
            await _unitOfWork.SaveInternalAsync(cancellationToken);

            return resource.ResourceId;
        }
    }
}
