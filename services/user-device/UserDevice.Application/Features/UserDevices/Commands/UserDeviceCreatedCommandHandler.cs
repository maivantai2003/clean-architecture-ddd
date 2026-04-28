using MediatR;
using UserDevice.Application.Abstractions.Persistence;

namespace UserDevice.Application.Features.UserDevices.Commands
{
    public class UserDeviceCreatedCommandHandler:IRequestHandler<UserDeviceCreatedCommand,Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserDeviceCreatedCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Guid> Handle(UserDeviceCreatedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
