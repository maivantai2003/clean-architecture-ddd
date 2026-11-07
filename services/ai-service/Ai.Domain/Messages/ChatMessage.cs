using Ai.Domain.Common;
using Ai.Domain.Messages.ValueObjects;

namespace Ai.Domain.Messages
{
    public sealed class ChatMessage : AggregateRoot
    {
        public Guid ConversationId { get; private set; }

        public MessageRole Role { get; private set; }

        public MessageContent? Content { get; private set; }

        public MessageStatus Status { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? CompletedAt { get; private set; }

        public string? FailureCode { get; private set; }

        private ChatMessage()
        {
        }

        private ChatMessage(
            Guid id,
            Guid conversationId,
            MessageRole role,
            MessageContent? content,
            DateTimeOffset now) : base(id)
        {
            if (conversationId == Guid.Empty)
                throw new DomainException("ConversationId không hợp lệ.");

            ConversationId = conversationId;
            Role = role;
            Content = content;
            Status = role == MessageRole.User
                ? MessageStatus.Completed
                : MessageStatus.Pending;

            CreatedAt = now;

            if (Status == MessageStatus.Completed)
                CompletedAt = now;
        }

        public static ChatMessage CreateUserMessage(
            Guid conversationId,string content)
        {
            return new ChatMessage(
                Guid.NewGuid(),
                conversationId,
                MessageRole.User,
                MessageContent.Create(content),
                DateTimeOffset.UtcNow);
        }

        public static ChatMessage CreateAssistantMessage(Guid conversationId)
        {
            return new ChatMessage(Guid.NewGuid(),conversationId,MessageRole.Assistant,null,DateTimeOffset.UtcNow);
        }

        public void Complete(string content)
        {
            EnsurePending();

            Content = MessageContent.Create(content);
            Status = MessageStatus.Completed;
            CompletedAt = DateTimeOffset.UtcNow;
            FailureCode = null;
        }

        public void Fail(string failureCode)
        {
            EnsurePending();

            if (string.IsNullOrWhiteSpace(failureCode))
                throw new DomainException("FailureCode không hợp lệ.");

            Status = MessageStatus.Failed;
            FailureCode = failureCode.Trim();
        }

        private void EnsurePending()
        {
            if (Role != MessageRole.Assistant ||
                Status != MessageStatus.Pending)
            {
                throw new DomainException(
                    "Tin nhắn không ở trạng thái chờ xử lý.");
            }
        }
    }
}
