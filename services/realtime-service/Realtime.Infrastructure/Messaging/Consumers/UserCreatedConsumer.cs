using MassTransit;
using MediatR;
using Realtime.Application.Features.Commands;
using Shared.Contracts.Events;

namespace Realtime.Infrastructure.Messaging.Consumers
{
    public class UserCreatedConsumer:IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        public UserCreatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            var message = context.Message;
            await _mediator.Send(new UserCreatedCommand(message.Id, message.Name, message.Email));
        }
    }
}
