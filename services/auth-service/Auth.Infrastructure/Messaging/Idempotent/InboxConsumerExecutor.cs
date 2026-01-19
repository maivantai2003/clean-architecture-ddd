using Auth.Infrastructure.Inbox;
using Auth.Infrastructure.Persistence;

namespace Auth.Infrastructure.Messaging.Idempotent
{
    public class InboxConsumerExecutor:IIdempotentExecutor
    {
        private readonly IInboxMessageRepository _inbox;
        private readonly AuthDbContext _context;

        public InboxConsumerExecutor(IInboxMessageRepository inbox,AuthDbContext db){
            _inbox = inbox;
            _context = db;
        }

        public async Task ExecuteAsync(string messageId,string consumer,Func<Task> handler,CancellationToken ct){
            if (await _inbox.ExistsAsync(messageId, consumer, ct))
                return;

            await using var tx = await _context.Database.BeginTransactionAsync(ct);

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
