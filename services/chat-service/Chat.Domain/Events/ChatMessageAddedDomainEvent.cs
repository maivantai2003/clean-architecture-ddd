using Chat.Domain.Common;

namespace Chat.Domain.Events
{
    public sealed class ChatMessageAddedDomainEvent : DomainEvent
    {
        public Guid ChatId { get; init; }
        public Guid MessageId { get; init; }
        public Guid SenderId { get; init; }
        public string Content { get; init; } = string.Empty;
        public bool IsBot { get; init; }
        public DateTime SentAt { get; init; }

        public ChatMessageAddedDomainEvent(Guid chatId, Guid messageId, Guid senderId, string content, bool isBot, DateTime sentAt)
        {
            ChatId = chatId;
            MessageId = messageId;
            SenderId = senderId;
            Content = content;
            IsBot = isBot;
            SentAt = sentAt;
        }
    }
}
