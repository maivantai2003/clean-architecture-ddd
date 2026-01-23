using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");
            builder.HasKey(p => p.PermissionId);  
            builder.Property(p => p.Code)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasIndex(builder => builder.Code).IsUnique();
            builder.Property(x => x.ApiEndpoint)
               .HasMaxLength(200);
            builder.Property(x => x.HttpMethod)
                   .HasMaxLength(10);
            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);
            builder.HasOne(p => p.Resource)
                  .WithMany()
                  .HasForeignKey(p => p.ResourceId)
                  .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Action)
                   .WithMany()
                   .HasForeignKey(p => p.ActionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
