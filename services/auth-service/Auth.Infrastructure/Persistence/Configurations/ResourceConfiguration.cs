
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("resources");
            builder.HasKey(r => r.ResourceId);
            builder.Property(r => r.ResourceCode)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasIndex(builder => builder.ResourceCode).IsUnique();
        }
    }
}
