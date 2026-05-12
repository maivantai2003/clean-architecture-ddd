namespace Shared.Contracts.Events
{
    public sealed class ChatCreatedIntegrationEvent
    {
        public Guid ChatId { get; init; }
        public string ChatType { get; init; } = default!;
        public IReadOnlyCollection<Guid> ParticipantIds { get; init; } = Array.Empty<Guid>();
        public DateTime CreatedAt { get; init; }
    }
}
