namespace Chat.Application.DTOs
{
    public sealed class ChatDto
    {
        public Guid ChatId { get; init; }
        public string ChatType { get; init; } = string.Empty;
        public IReadOnlyCollection<Guid> ParticipantIds { get; init; } = Array.Empty<Guid>();
        public IReadOnlyCollection<ChatMessageDto> Messages { get; init; } = Array.Empty<ChatMessageDto>();
        public DateTime CreatedAt { get; init; }
    }
}
