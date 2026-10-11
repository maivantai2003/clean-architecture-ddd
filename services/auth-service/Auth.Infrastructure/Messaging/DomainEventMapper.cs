using Auth.Application.Abstractions.Persistence;
using Shared.Contracts.Events;
using Auth.Domain.Users.Events;
using Auth.Domain.PasswordResetToken.Events;
namespace Auth.Infrastructure.Messaging
{
    public class DomainEventMapper : IDomainEventMapper
    {
        public object Map(object domainEvent)
        {
            return domainEvent switch
            {
                UserCreatedDomainEvent e => new UserRegisteredIntegrationEvent(e.UserId, e.Email, e.Name),
                PasswordResetRequestedDomainEvent e => new PasswordResetRequestedIntegrationEvent(e.UserId, e.Email, e.ResetToken, e.ExpiresAtUtc),
                _ => throw new NotSupportedException(
                    $"No mapping for {domainEvent.GetType().Name}")
            };
        }
    }
}
