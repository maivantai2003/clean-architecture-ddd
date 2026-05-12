using Chat.Application.Abstractions.Persistence;
using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public sealed class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _context;

        public ChatRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid chatId)
        {
            return await _context.Chats.AnyAsync(c => c.ChatId == chatId);
        }

        public async Task<Chat?> GetByIdAsync(Guid chatId)
        {
            return await _context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.ChatId == chatId);
        }

        public async Task<List<Chat>> GetByParticipantIdAsync(Guid participantId)
        {
            var chats = await _context.Chats.Include(c => c.Messages).ToListAsync();
            return chats.Where(c => c.ParticipantIds.Contains(participantId)).ToList();
        }

        public void Update(Chat chat)
        {
            _context.Chats.Update(chat);
            _context.SaveChanges();
        }
    }
}
