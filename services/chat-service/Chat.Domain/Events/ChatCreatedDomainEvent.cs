using Chat.Domain.Enums;
using Chat.Domain.Common;

namespace Chat.Domain.Events
{
    public sealed class ChatCreatedDomainEvent : DomainEvent
    {
        public Guid ChatId { get; init; }
        public ChatType ChatType { get; init; }
        public IReadOnlyCollection<Guid> ParticipantIds { get; init; } = Array.Empty<Guid>();
        public DateTime CreatedAt { get; init; }

        public ChatCreatedDomainEvent(Guid chatId, ChatType chatType, IReadOnlyCollection<Guid> participantIds, DateTime createdAt)
        {
            ChatId = chatId;
            ChatType = chatType;
            ParticipantIds = participantIds;
            CreatedAt = createdAt;
        }
    }
}
