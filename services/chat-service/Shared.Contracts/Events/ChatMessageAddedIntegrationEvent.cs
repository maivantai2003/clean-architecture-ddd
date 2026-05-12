namespace Shared.Contracts.Events
{
    public sealed class ChatMessageAddedIntegrationEvent
    {
        public Guid ChatId { get; init; }
        public Guid MessageId { get; init; }
        public Guid SenderId { get; init; }
        public string Content { get; init; } = default!;
        public bool IsBot { get; init; }
        public DateTime SentAt { get; init; }
    }
}
