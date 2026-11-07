using Chat.Domain.Entities;
namespace Chat.Application.Abstractions.Persistence
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(Guid chatId);
        Task<List<Conversation>> GetByParticipantIdAsync(Guid participantId);
        Task AddAsync(Conversation chat);
        void Update(Conversation chat);
        Task<bool> ExistsAsync(Guid chatId);
    }
}
