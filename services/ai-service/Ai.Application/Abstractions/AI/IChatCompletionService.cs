namespace Ai.Application.Abstractions.AI
{
    public sealed record ChatTurn(
    string Role,
    string Content);

    public sealed record ChatCompletionRequest(
        IReadOnlyList<ChatTurn> Messages,
        string? Instructions = null);

    public sealed record ChatCompletionResponse(
        string Content,
        string? Model = null,
        int? InputTokens = null,
        int? OutputTokens = null);

    public interface IChatCompletionService
    {
        Task<ChatCompletionResponse> CompleteAsync(
            ChatCompletionRequest request,
            CancellationToken cancellationToken);
    }
}
