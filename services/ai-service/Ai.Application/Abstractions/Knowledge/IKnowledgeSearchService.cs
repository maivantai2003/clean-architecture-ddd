namespace Ai.Application.Abstractions.Knowledge
{
    public sealed record RetrievedChunk(
    Guid DocumentId,string Text,double Score,string? SourceName);

    public sealed record KnowledgeSearchRequest(Guid UserId,Guid? TenantId,string Query,int TopK);
    public interface IKnowledgeSearchService
    {
        Task<IReadOnlyList<RetrievedChunk>> SearchAsync(KnowledgeSearchRequest request,
            CancellationToken cancellationToken);
    }
}
