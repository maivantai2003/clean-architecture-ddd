using Ai.Domain.Messages;
namespace Ai.Application.Abstractions.Persistence
{
    public interface IChatMessageRepository
    {
        Task AddAsync(
            ChatMessage message,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<ChatMessage>> GetRecentAsync(
            Guid conversationId,
            int limit,
            CancellationToken cancellationToken);
    }
}
