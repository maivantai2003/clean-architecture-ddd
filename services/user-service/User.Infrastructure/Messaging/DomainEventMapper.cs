
//using User.Contracts.Events;
using Shared.Contracts.Events;
using User.Application.Abstractions.Persistence;
using User.Domain.Users.Events;

namespace User.Infrastructure.Messaging
{
    public class DomainEventMapper : IDomainEventMapper
    {
       
        public object Map(object domainEvent)
        {
            return domainEvent switch
            {
                UserEmailUpdatedDomainEvent e => new UserUpdateEmailIntegrationEvent(
                    e.UserId,
                    e.NewEmail
                ),
            };
        }
    }
}
