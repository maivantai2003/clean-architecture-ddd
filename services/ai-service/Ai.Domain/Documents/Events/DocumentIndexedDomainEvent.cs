using Ai.Domain.Common;

namespace Ai.Domain.Documents.Events
{
    public sealed class DocumentIndexedDomainEvent(
    Guid DocumentId,
    Guid KnowledgeBaseId,
    DateTimeOffset OccurredOn
) : DomainEvent;
}
