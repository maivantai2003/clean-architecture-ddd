using Chat.Application.Abstractions.Persistence;
using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public sealed class ConversationRepository : IConversationRepository
    {
        private readonly ConversationDbContext _context;

        public ConversationRepository(ConversationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Conversation conversation)
        {
            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid chatId)
        {
            return await _context.Conversations.AnyAsync(c => c.ChatId == chatId);
        }

        public async Task<Conversation?> GetByIdAsync(Guid chatId)
        {
            return await _context.Conversations.Include(c => c.Messages).FirstOrDefaultAsync(c => c.ChatId == chatId);
        }

        public async Task<List<Conversation>> GetByParticipantIdAsync(Guid participantId)
        {
            var conversations = await _context.Conversations.Include(c => c.Messages).ToListAsync();
            return conversations.Where(c => c.ParticipantIds.Contains(participantId)).ToList();
        }

        public void Update(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
            _context.SaveChanges();
        }
    }
}
