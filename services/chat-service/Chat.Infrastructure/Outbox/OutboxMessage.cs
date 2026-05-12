namespace Chat.Infrastructure.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public DateTime OccurredOn { get; private set; }

        private OutboxMessage() { }

        public OutboxMessage(object domainEvent)
        {
            Id = Guid.NewGuid();
            Type = domainEvent.GetType().AssemblyQualifiedName ?? domainEvent.GetType().FullName ?? string.Empty;
            Content = System.Text.Json.JsonSerializer.Serialize(domainEvent, domainEvent.GetType());
            OccurredOn = DateTime.UtcNow;
        }
    }
}
