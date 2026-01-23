//using User.Application.Outbox;
//using User.Domain.Common;
//using User.Infrastructure.Persistence;

//namespace User.Infrastructure.Outbox
//{
//    public class OutboxRepository: IOutboxRepository
//    {
//        private readonly UserDbContext _context;
//        public OutboxRepository(UserDbContext context)
//        {
//            _context = context;
//        }
//        public async Task SaveEventAsync(DomainEvent domainEvent)
//        {
//            var outboxMessage = new OutboxMessage(domainEvent);
//            await _context.OutboxMessages.AddAsync(outboxMessage);
//        }
//    }
//}
