using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Auth.Infrastructure.Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles"); 
            builder.HasKey(ur => ur.UserRoleId);
            builder.HasIndex(x => new { x.UserId, x.RoleId })
               .IsUnique();
        }
    }
}
