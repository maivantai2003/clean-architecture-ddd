using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Inbox
{
    public class InboxMessageRepository:IInboxMessageRepository
    {
        private readonly UserDbContext _context;
        public InboxMessageRepository(UserDbContext context)
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
