using Ai.Domain.Common;

namespace Ai.Domain.Documents.Events
{
    public class DocumentUploadedDomainEvent:DomainEvent
    {
        public Guid DocumentId { get; }
        public Guid KnowledgeBaseId { get; }
        public DateTimeOffset OccurredOn { get; }

        public DocumentUploadedDomainEvent(Guid documentId, Guid knowledgeBaseId, DateTimeOffset occurredOn)
        {
            DocumentId = documentId;
            KnowledgeBaseId = knowledgeBaseId;
            OccurredOn = occurredOn;
        }
    }
}
