using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Messaging.Idempotent;
using User.Infrastructure.Inbox;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Messaging.Idempotent
{
    public class InboxConsumerExecutor:IIdempotentExecutor
    {
        private readonly IInboxMessageRepository _inbox;
        private readonly UserDbContext _context;

        public InboxConsumerExecutor(IInboxMessageRepository inbox,UserDbContext db){
            _inbox = inbox;
            _context = db;
        }

        public async Task ExecuteAsync(string messageId,string consumer,Func<Task> handler,CancellationToken ct){
            if (await _inbox.ExistsAsync(messageId, consumer, ct))
                return;

            await using var tx = await _context.Database.BeginTransactionAsync(ct);
            //await handler();

            //await _inbox.AddAsync(
            //    new InboxMessage(messageId, consumer),
            //    ct
            //);

            //await tx.CommitAsync(ct);
            try
            {
                if (await _inbox.ExistsAsync(messageId, consumer, ct))
                {
                    await tx.RollbackAsync(ct);
                    return;
                }
                await handler();
                await _inbox.AddAsync(new InboxMessage(messageId, consumer), ct);
                await _context.SaveChangesAsync(ct);
                await tx.CommitAsync(ct);
            }
            catch
            {
                await tx.RollbackAsync(ct);
                throw;
            }
        }
    }
}
