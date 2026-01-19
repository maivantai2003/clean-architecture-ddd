namespace User.Infrastructure.Inbox
{
    public interface IInboxMessageRepository
    {
        Task<bool> ExistsAsync(string messageId, string consumer, CancellationToken ct);
        Task AddAsync(InboxMessage message, CancellationToken ct);
    }
}
