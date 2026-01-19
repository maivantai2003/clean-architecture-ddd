using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Auth.Infrastructure.Persistence.Configurations
{
    public class ActionConfiguration : IEntityTypeConfiguration<Domain.Entities.Action>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Action> builder)
        {
            builder.ToTable("actions");
            builder.HasKey(a => a.ActionId);
            builder.Property(a => a.ActionCode)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasIndex(builder => builder.ActionCode).IsUnique(); 
        }
    }
}
