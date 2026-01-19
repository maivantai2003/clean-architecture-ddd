using Auth.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Auth.Infrastructure.Persistence.Repositories
{
    public class ActionRepository:IActionRepository
    {
        private readonly AuthDbContext _context;
        public ActionRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.Action action)
        {
            await _context.Actions.AddAsync(action);
        }

        public async Task<List<Domain.Entities.Action>> GetAllAsync()
        {
           return await _context.Actions.ToListAsync();
        }
        public async Task<Domain.Entities.Action?> GetByCodeAsync(string code)
        {
            return await _context.Actions.FirstOrDefaultAsync(a => a.ActionCode == code);   
        }

        public async Task<Domain.Entities.Action?> GetByIdAsync(Guid id)
        {
           return await _context.Actions.FindAsync(id);
        }

        public void Remove(Domain.Entities.Action action)
        {
            _context.Actions.Remove(action);
        }
    }
}
