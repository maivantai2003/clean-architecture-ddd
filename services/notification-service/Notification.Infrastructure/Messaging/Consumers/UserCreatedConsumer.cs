using MassTransit;
using MediatR;
using Notification.Application.Features.Emails.Commands;
using Notification.Infrastructure.Messaging.Idempotent;
using Shared.Contracts.Events;
namespace Notification.Infrastructure.Messaging.Consumers
{
    public class UserCreatedConsumer:IConsumer<UserRegisteredIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly IIdempotentExecutor _executor;
        public UserCreatedConsumer(IMediator mediator, IIdempotentExecutor executor){
            _mediator = mediator;
            _executor = executor;
        }

        public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            if (context.MessageId == null)
                throw new InvalidOperationException(
                    "MessageId is required for Inbox (idempotent consumer)");
            var messageId = context.MessageId.Value.ToString();
            var message= context.Message;
            await _executor.ExecuteAsync(
                messageId:messageId,
                consumer:nameof(UserCreatedConsumer),
                handler:() =>
                _mediator.Send(new SendWelcomeEmailCommand(message.Email,message.Username),
                  context.CancellationToken
                ),ct:context.CancellationToken
            );
            //Console.WriteLine(message.Email);
            //await _mediator.Send(new SendWelcomeEmailCommand(message.Email,message.Username));
        }
    }
}
