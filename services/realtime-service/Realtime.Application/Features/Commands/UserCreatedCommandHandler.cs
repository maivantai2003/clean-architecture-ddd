using MediatR;
using Realtime.Application.Interfaces;

namespace Realtime.Application.Features.Commands
{
    public class UserCreatedCommandHandler : IRequestHandler<UserCreatedCommand,Unit>
    {
        private readonly IRealtimePublisher _publisher;
        public UserCreatedCommandHandler(IRealtimePublisher publisher)
        {
            _publisher = publisher; 
        }

        public async Task<Unit> Handle(UserCreatedCommand request, CancellationToken cancellationToken)
        {
            await _publisher.BroadcastAsync("user-created",new { request.Id, request.Name, request.Email });
            return Unit.Value;
        }
    }
}
