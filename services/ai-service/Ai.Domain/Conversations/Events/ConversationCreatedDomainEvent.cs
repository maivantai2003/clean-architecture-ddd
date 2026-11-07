using Ai.Domain.Common;

namespace Ai.Domain.Conversations.Events
{
    public class ConversationCreatedDomainEvent : DomainEvent
    {
       public Guid ConversationId { get; }
       public Guid UserId { get; }
       public DateTimeOffset OccurredOn { get; }

       public ConversationCreatedDomainEvent(Guid conversationId, Guid userId, DateTimeOffset occurredOn)
       {
           ConversationId = conversationId;
           UserId = userId;
           OccurredOn = occurredOn;
       }
    }
}
