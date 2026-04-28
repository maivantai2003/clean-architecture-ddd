namespace Search.Infrastructure.Inbox
{
    public class InboxMessage
    {
        public Guid Id { get; private set; }
        public string MessageId { get; private set; } = null!;
        public string Consumer { get; private set; } = null!;
        public DateTime ProcessedAt { get; private set; }

        private InboxMessage() { }

        public InboxMessage(string messageId, string consumer)
        {
            Id = Guid.NewGuid();
            MessageId = messageId;
            Consumer = consumer;
            ProcessedAt = DateTime.UtcNow;
        }
    }
}
