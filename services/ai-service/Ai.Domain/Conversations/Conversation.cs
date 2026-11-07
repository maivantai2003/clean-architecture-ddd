using Ai.Domain.Common;
using Ai.Domain.Conversations.Events;

namespace Ai.Domain.Conversations
{
    public sealed class Conversation : AggregateRoot
    {
        public Guid UserId { get; private set; }

        public Guid? TenantId { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public ConversationStatus Status { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        private Conversation()
        {
        }

        private Conversation(
            Guid id,
            Guid userId,
            Guid? tenantId,
            string title,
            DateTimeOffset now) : base(id)
        {
            if (userId == Guid.Empty)
                throw new DomainException("UserId không hợp lệ.");

            if (tenantId == Guid.Empty)
                throw new DomainException("TenantId không hợp lệ.");

            UserId = userId;
            TenantId = tenantId;
            Title = NormalizeTitle(title);
            Status = ConversationStatus.Active;
            CreatedAt = now;
            UpdatedAt = now;
            AddDomainEvent(new ConversationCreatedDomainEvent(Id, UserId, now));
        }

        public static Conversation Create(
            Guid userId,
            string title,
            Guid? tenantId = null)
        {
            return new Conversation(
                Guid.NewGuid(),
                userId,
                tenantId,
                title,
                DateTimeOffset.UtcNow);
        }

        public void Rename(string title)
        {
            EnsureActive();

            Title = NormalizeTitle(title);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Archive()
        {
            if (Status == ConversationStatus.Archived)
                return;

            Status = ConversationStatus.Archived;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void EnsureActive()
        {
            if (Status != ConversationStatus.Active)
            {
                throw new DomainException(
                    "Không thể thao tác trên hội thoại đã lưu trữ.");
            }
        }

        private static string NormalizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Tiêu đề không được để trống.");

            title = title.Trim();

            if (title.Length > 200)
                throw new DomainException(
                    "Tiêu đề không được vượt quá 200 ký tự.");

            return title;
        }
    }
}
