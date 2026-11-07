namespace Chat.Application.DTOs
{
    public sealed class ConversationDto
    {
        public Guid ChatId { get; init; }
        public string ChatType { get; init; } = string.Empty;
        public IReadOnlyCollection<Guid> ParticipantIds { get; init; } = Array.Empty<Guid>();
        public IReadOnlyCollection<MessageDto> Messages { get; init; } = Array.Empty<MessageDto>();
        public DateTime CreatedAt { get; init; }
    }
}
