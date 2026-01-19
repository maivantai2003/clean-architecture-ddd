using Auth.Application.Features.Users.Commands;
using Auth.Infrastructure.Messaging.Idempotent;
using MassTransit;
using MediatR;
using Shared.Contracts.Events;

namespace Auth.Infrastructure.Messaging.Consumers
{
    public class UserUpdateEmailConsumer : IConsumer<UserUpdateEmailIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly IIdempotentExecutor _executor;
        public UserUpdateEmailConsumer(IMediator mediator, IIdempotentExecutor inboxConsumerExecutor)
        {
            _mediator = mediator;
            _executor = inboxConsumerExecutor;
        }
        public async Task Consume(ConsumeContext<UserUpdateEmailIntegrationEvent> context)
        {
            if (context.MessageId == null)
                throw new InvalidOperationException(
                    "MessageId is required for Inbox (idempotent consumer)");
            var messageId = context.MessageId.Value.ToString();
            await _executor.ExecuteAsync(
            messageId: messageId,
            consumer: nameof(UserUpdateEmailConsumer),

            handler: () =>
                _mediator.Send(
                    new UserUpdateEmailCommand(context.Message.UserId, context.Message.NewEmail),
                    context.CancellationToken
                ),

                ct: context.CancellationToken
            );
        }
    }
}
