using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Domain.Enums;

namespace User.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                   .ValueGeneratedNever();

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(u => u.Username)
                   .IsUnique();

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.AvatarUrl)
                   .HasMaxLength(500);

            builder.Property(u => u.Gender)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(u => u.Status)
                   .HasConversion<int>()
                   .IsRequired()
                   .HasDefaultValue(UserStatus.Active);
            builder.HasQueryFilter(u => u.Status != UserStatus.Deleted);

        }
    }
}
