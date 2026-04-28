using Search.Infrastructure.Inbox;
using Search.Infrastructure.Persistence;

namespace Search.Infrastructure.Messaging.Idempotent
{
    public class InboxConsumerExecutor:IIdempotentExecutor
    {
        private readonly IInboxMessageRepository _inbox;
        private readonly SearchDbContext _context;

        public InboxConsumerExecutor(IInboxMessageRepository inbox,SearchDbContext db){
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
