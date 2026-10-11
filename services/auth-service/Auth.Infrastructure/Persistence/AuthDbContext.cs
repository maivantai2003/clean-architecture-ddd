using Auth.Domain.Common;
using Auth.Domain.Entities;
using Auth.Infrastructure.Inbox;
using Auth.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<PermissionAction> AppActions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore(typeof(DomainEvent));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
