using Ai.Domain.Common;

namespace Ai.Domain.KnowledgeBases
{
    public sealed class KnowledgeBase : AggregateRoot
    {
        public Guid OwnerId { get; private set; }

        public Guid? TenantId { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        private KnowledgeBase()
        {
        }

        private KnowledgeBase(
            Guid id,
            Guid ownerId,
            Guid? tenantId,
            string name,
            string? description,
            DateTimeOffset now) : base(id)
        {
            if (ownerId == Guid.Empty)
                throw new DomainException("OwnerId không hợp lệ.");

            if (tenantId == Guid.Empty)
                throw new DomainException("TenantId không hợp lệ.");

            OwnerId = ownerId;
            TenantId = tenantId;
            Name = NormalizeName(name);
            Description = NormalizeDescription(description);
            IsActive = true;
            CreatedAt = now;
        }

        public static KnowledgeBase Create(
            Guid ownerId,
            string name,
            string? description = null,
            Guid? tenantId = null)
        {
            return new KnowledgeBase(
                Guid.NewGuid(),
                ownerId,
                tenantId,
                name,
                description,
                DateTimeOffset.UtcNow);
        }

        public void Rename(string name)
        {
            EnsureActive();
            Name = NormalizeName(name);
        }

        public void UpdateDescription(string? description)
        {
            EnsureActive();
            Description = NormalizeDescription(description);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void EnsureActive()
        {
            if (!IsActive)
                throw new DomainException(
                    "Kho tri thức hiện không hoạt động.");
        }

        private static string NormalizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(
                    "Tên kho tri thức không được để trống.");

            name = name.Trim();

            if (name.Length > 150)
                throw new DomainException(
                    "Tên kho tri thức không được vượt quá 150 ký tự.");

            return name;
        }

        private static string? NormalizeDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return null;

            description = description.Trim();

            if (description.Length > 2_000)
                throw new DomainException(
                    "Mô tả không được vượt quá 2000 ký tự.");

            return description;
        }
    }
}
