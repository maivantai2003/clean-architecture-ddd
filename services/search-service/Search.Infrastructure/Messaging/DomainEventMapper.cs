
////using User.Contracts.Events;
//using Shared.Contracts.Events;
//using Search.Application.Abstractions.Persistence;
//using Search.Domain.Users.Events;

//namespace Search.Infrastructure.Messaging
//{
//    public class DomainEventMapper : IDomainEventMapper
//    {
       
//        public object Map(object domainEvent)
//        {
//            return domainEvent switch
//            {
//                UserEmailUpdatedDomainEvent e => new UserUpdateEmailIntegrationEvent(
//                    e.UserId,
//                    e.NewEmail
//                ),
//            };
//        }
//    }
//}
