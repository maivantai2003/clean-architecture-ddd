using Auth.Application.Outbox;
using Auth.Domain.Common;
using Auth.Infrastructure.Persistence;

namespace Auth.Infrastructure.Outbox
{
    public class OutboxRepository: IOutboxRepository
    {
        private readonly AuthDbContext _context;
        public OutboxRepository(AuthDbContext context)
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
