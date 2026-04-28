using Search.Application.Outbox;
using Search.Domain.Common;
using Search.Infrastructure.Persistence;

namespace Search.Infrastructure.Outbox
{
    public class OutboxRepository: IOutboxRepository
    {
        private readonly SearchDbContext _context;
        public OutboxRepository(SearchDbContext context)
        {
            _context = context;
        }
        public async Task SaveEventAsync(DomainEvent domainEvent)
        {
            var outboxMessage = new OutboxMessage(domainEvent);
            await _context.OutboxMessages.AddAsync(outboxMessage);
        }
    }
}
