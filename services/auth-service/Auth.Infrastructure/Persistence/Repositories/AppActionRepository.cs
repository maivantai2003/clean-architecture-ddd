using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Auth.Infrastructure.Persistence.Repositories
{
    public class AppActionRepository:IAppActionRepository
    {
        private readonly AuthDbContext _context;
        public AppActionRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PermissionAction action)
        {
            await _context.AppActions.AddAsync(action);
        }

        public async Task<List<PermissionAction>> GetAllAsync()
        {
           return await _context.AppActions.ToListAsync();
        }
        public async Task<PermissionAction?> GetByCodeAsync(string code)
        {
            return await _context.AppActions.FirstOrDefaultAsync(a => a.ActionCode == code);   
        }

        public async Task<PermissionAction?> GetByIdAsync(Guid id)
        {
           return await _context.AppActions.FindAsync(id);
        }

        public void Remove(PermissionAction action)
        {
            _context.AppActions.Remove(action);
        }
    }
}
