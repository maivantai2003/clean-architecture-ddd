namespace Ai.Application.Abstractions.AI
{
    public interface IEmbeddingService
    {
        Task<IReadOnlyList<float[]>> CreateEmbeddingsAsync(
            IReadOnlyList<string> texts,
            CancellationToken cancellationToken);
    }
}
