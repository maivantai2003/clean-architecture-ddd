using MassTransit;
using MediatR;
using User.Application.Features.Users.Commands;
using Shared.Contracts.Events;
using User.Infrastructure.Messaging.Idempotent;

namespace User.Infrastructure.Messaging.Consumers
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
                await _executor.ExecuteAsync(
                    messageId: messageId,
                    consumer: nameof(UserCreatedConsumer),

                    handler: () =>
                        _mediator.Send(
                            new UserRegisterCommand(
                                context.Message.UserId
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
