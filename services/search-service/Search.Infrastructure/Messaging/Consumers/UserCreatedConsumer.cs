using MassTransit;
using MediatR;
using Search.Application.Features.Search.Commands;
using Search.Infrastructure.Messaging.Idempotent;
using Shared.Contracts.Events;

namespace Search.Infrastructure.Messaging.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserRegisteredIntegrationEvent>
    {
        private readonly IMediator _mediator;

        private readonly IIdempotentExecutor _executor;
        public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            if (context.MessageId==null)
            {
                throw new InvalidOperationException(
                        "MessageId is required for Inbox (idempotent consumer)");
            }
            var messageId = context.MessageId.Value.ToString();
            await _executor.ExecuteAsync(
                messageId: messageId,
                consumer: nameof(UserCreatedConsumer),

                handler: () =>
                    _mediator.Send(
                        new IndexUserCommand(
                            context.Message.UserId.ToString()
                            , context.Message.Username
                            , context.Message.Email
                        ),
                        context.CancellationToken
                    ),

                    ct: context.CancellationToken
            );
        }
    }
}
