using Microsoft.EntityFrameworkCore;
using UserDevice.Domain.Entities;
using UserDevice.Infrastructure.Inbox;

namespace UserDevice.Infrastructure.Persistence
{
    public class UserDeviceDbContext:DbContext
    {
        public UserDeviceDbContext(DbContextOptions<UserDeviceDbContext> options) : base(options)
        {
        }
        public DbSet<UserDevice.Domain.Entities.UserDevice> userDevices { get; set; }
        public DbSet<DeviceOtp> DeviceOtps { get; set; }
        //public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InboxMessage> InboxMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore(typeof(DomainEvent));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDeviceDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
