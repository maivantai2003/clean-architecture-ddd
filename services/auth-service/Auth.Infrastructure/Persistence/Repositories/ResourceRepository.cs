using Auth.Application.Abstractions.Persistence;
using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class ResourceRepository:IResourceRepository
    {
        private readonly AuthDbContext _context;
        public ResourceRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<Resource?> GetByCodeAsync(string code)
        {
           return await _context.Resources.FirstOrDefaultAsync(r => r.ResourceCode == code);
        }

        public async Task<Resource?> GetByIdAsync(Guid id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public void Remove(Resource resource)
        {
            _context.Resources.Remove(resource);
        }
    }
}
