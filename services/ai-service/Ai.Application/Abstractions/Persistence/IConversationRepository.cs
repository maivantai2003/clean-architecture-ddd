using Ai.Domain.Conversations;
namespace Ai.Application.Abstractions.Persistence
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(
            Guid conversationId,
            CancellationToken cancellationToken);

        Task AddAsync(
            Conversation conversation,
            CancellationToken cancellationToken);
    }
}
