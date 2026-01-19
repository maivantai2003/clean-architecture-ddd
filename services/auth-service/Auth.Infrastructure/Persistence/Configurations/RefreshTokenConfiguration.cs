using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.HasKey(x => x.RefreshTokenId);

            builder.Property(x => x.TokenHash)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasIndex(x => x.TokenHash)
                   .IsUnique();
            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.IsRevoked)
                   .HasDefaultValue(false);
            //builder.Property(x => x.CreatedAt)
            //       .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
