using Microsoft.EntityFrameworkCore;
using Auth.Infrastructure.Persistence;

namespace Auth.Infrastructure.Inbox
{
    public class InboxMessageRepository:IInboxMessageRepository
    {
        private readonly AuthDbContext _context;
        public InboxMessageRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(InboxMessage message, CancellationToken ct)
        {
            await _context.InboxMessages.AddAsync(message,ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<bool> ExistsAsync(string messageId, string consumer, CancellationToken ct)
        {
            return await _context.InboxMessages.AnyAsync(x => x.MessageId == messageId && x.Consumer == consumer,ct);
        }
    }
}
