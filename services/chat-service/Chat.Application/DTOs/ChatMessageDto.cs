namespace Chat.Application.DTOs
{
    public sealed class ChatMessageDto
    {
        public Guid MessageId { get; init; }
        public Guid SenderId { get; init; }
        public string Content { get; init; } = string.Empty;
        public bool IsBot { get; init; }
        public DateTime SentAt { get; init; }
    }
}
