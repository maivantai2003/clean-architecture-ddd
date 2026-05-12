using Chat.Domain.Common;
using Chat.Domain.Enums;
using Chat.Domain.Events;

namespace Chat.Domain.Entities
{
    public sealed class Chat : AggregateRoot
    {
        public Guid ChatId { get; private set; }
        public ChatType Type { get; private set; }
        public List<Guid> ParticipantIds { get; private set; } = new();
        public List<ChatMessage> Messages { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }

        private Chat() { }

        private Chat(Guid chatId, ChatType type, IEnumerable<Guid> participantIds)
        {
            ChatId = chatId;
            Type = type;
            ParticipantIds = participantIds.ToList();
            CreatedAt = DateTime.UtcNow;
            AddDomainEvent(new ChatCreatedDomainEvent(ChatId, Type, ParticipantIds, CreatedAt));
        }

        public static Chat CreateUserToUserChat(IEnumerable<Guid> participants)
        {
            var addresses = participants?.ToList() ?? throw new ArgumentNullException(nameof(participants));
            if (addresses.Count != 2)
            {
                throw new InvalidOperationException("User-to-user chat requires exactly two participants.");
            }
            return new Chat(Guid.NewGuid(), ChatType.UserToUser, addresses);
        }

        public static Chat CreateUserToBotChat(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new InvalidOperationException("Bot chat requires a valid user identifier.");
            }
            return new Chat(Guid.NewGuid(), ChatType.UserToBotAI, new[] { userId });
        }

        public void AddMessage(Guid senderId, string content, bool isBot = false)
        {
            if (senderId == Guid.Empty)
            {
                throw new InvalidOperationException("SenderId must be a valid user id.");
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidOperationException("Message content cannot be empty.");
            }

            if (Type == ChatType.UserToUser && !ParticipantIds.Contains(senderId))
            {
                throw new InvalidOperationException("Sender must be a participant of the chat.");
            }

            if (Type == ChatType.UserToBotAI && !ParticipantIds.Contains(senderId) && !isBot)
            {
                throw new InvalidOperationException("Only the user participant or the bot may send messages in a user-to-bot chat.");
            }

            var message = new ChatMessage(senderId, content.Trim(), isBot);
            Messages.Add(message);
            AddDomainEvent(new ChatMessageAddedDomainEvent(ChatId, message.MessageId, senderId, content.Trim(), isBot, message.SentAt));
        }
    }
}
