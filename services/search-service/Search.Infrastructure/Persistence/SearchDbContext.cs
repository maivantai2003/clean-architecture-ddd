using Microsoft.EntityFrameworkCore;
using Search.Domain.Common;
using Search.Infrastructure.Inbox;
using Search.Infrastructure.Outbox;

namespace Search.Infrastructure.Persistence
{
    public class SearchDbContext:DbContext
    {
        public SearchDbContext(DbContextOptions<SearchDbContext> options) : base(options)
        {
        }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore(typeof(DomainEvent));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SearchDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
