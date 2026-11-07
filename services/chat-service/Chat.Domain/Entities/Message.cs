namespace Chat.Domain.Entities
{
    public sealed class Message
    {
        public Guid MessageId { get; private set; }
        public Guid SenderId { get; private set; }
        public string Content { get; private set; } = string.Empty;
        public bool IsBot { get; private set; }
        public DateTime SentAt { get; private set; }

        private Message() { }

        public Message(Guid senderId, string content, bool isBot)
        {
            MessageId = Guid.NewGuid();
            SenderId = senderId;
            Content = content;
            IsBot = isBot;
            SentAt = DateTime.UtcNow;
        }
    }
}
