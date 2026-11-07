using Ai.Domain.KnowledgeBases;
namespace Ai.Application.Abstractions.Persistence
{
    public interface IKnowledgeBaseRepository
    {
        Task<KnowledgeBase?> GetByIdAsync(
            Guid knowledgeBaseId,
            CancellationToken cancellationToken);
    }
}
