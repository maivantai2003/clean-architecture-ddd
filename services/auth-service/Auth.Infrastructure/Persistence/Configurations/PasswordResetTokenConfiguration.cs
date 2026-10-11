using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Configurations
{
    public class PasswordResetTokenConfiguration:IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.ToTable("password_reset_tokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.TokenHash)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.ExpiresAtUtc)
                .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();

            builder.Property(x => x.UsedAtUtc)
                .IsRequired(false);

            builder.HasIndex(x => x.TokenHash)
                .IsUnique();

            builder.HasIndex(x => x.UserId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
