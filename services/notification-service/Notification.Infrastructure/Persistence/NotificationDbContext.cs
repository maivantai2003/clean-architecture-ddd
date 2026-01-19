using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Inbox;

namespace Notification.Infrastructure.Persistence
{
    public class NotificationDbContext:DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options):base(options)
        {
        }
        public DbSet<InboxMessage> InboxMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
