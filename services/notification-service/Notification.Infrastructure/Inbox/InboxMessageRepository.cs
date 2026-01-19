using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Persistence;

namespace Notification.Infrastructure.Inbox
{
    public class InboxMessageRepository:IInboxMessageRepository
    {
        private readonly NotificationDbContext _context;
        public InboxMessageRepository(NotificationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(InboxMessage message, CancellationToken ct)
        {
            await _context.InboxMessages.AddAsync(message,ct);
            //await _context.SaveChangesAsync(ct);
        }
        public async Task<bool> ExistsAsync(string messageId, string consumer, CancellationToken ct)
        {
            return await _context.InboxMessages.AnyAsync(x => x.MessageId == messageId && x.Consumer == consumer,ct);
        }
    }
}
