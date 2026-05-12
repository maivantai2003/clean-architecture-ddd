using Chat.Domain.Entities;
namespace Chat.Application.Abstractions.Persistence
{
    public interface IChatRepository
    {
        Task<Chat?> GetByIdAsync(Guid chatId);
        Task<List<Chat>> GetByParticipantIdAsync(Guid participantId);
        Task AddAsync(Chat chat);
        void Update(Chat chat);
        Task<bool> ExistsAsync(Guid chatId);
    }
}
