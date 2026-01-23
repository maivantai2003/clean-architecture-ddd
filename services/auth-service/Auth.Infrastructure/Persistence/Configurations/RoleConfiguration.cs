using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");   
            builder.HasKey(r => r.RoleId);
            builder.Property(r => r.RoleCode)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasIndex(builder => builder.RoleCode).IsUnique();
            builder
           .HasMany(r => r.Permissions)
           .WithOne(rp => rp.Role)
           .HasForeignKey(rp => rp.RoleId)
           .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Permissions)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder
            //    .Navigation(r => r.Permissions)
            //    .HasField("_permissions")
            //    .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
