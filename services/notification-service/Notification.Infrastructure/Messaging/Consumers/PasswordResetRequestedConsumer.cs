using MassTransit;
using MediatR;
using Notification.Application.Features.Emails.Commands;
using Notification.Infrastructure.Messaging.Idempotent;
using Shared.Contracts.Events;

namespace Notification.Infrastructure.Messaging.Consumers
{
    public class PasswordResetRequestedConsumer(IMediator _mediator,IIdempotentExecutor _executor) : IConsumer<PasswordResetRequestedIntegrationEvent>
    {
        
        public async Task Consume(ConsumeContext<PasswordResetRequestedIntegrationEvent> context)
        {
            if(context.Message is null)
            {
                throw new ArgumentNullException(nameof(context.Message));
            }
            var messageId = context.MessageId.Value.ToString();
            await _executor.ExecuteAsync(
                messageId:messageId,
                consumer: nameof(PasswordResetRequestedConsumer),
                handler: () =>
               _mediator.Send(
               new SendPasswordResetEmailCommand(
                context.Message.Email,
                context.Message.ResetToken,
                context.Message.ExpiresAtUtc),
               context.CancellationToken),

                ct: context.CancellationToken
            );
        }
    }
}
