using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDevice.Domain.Entities;

namespace UserDevice.Infrastructure.Persistence.Configurations
{
    public class DeviceOtpConfiguration : IEntityTypeConfiguration<DeviceOtp>
    {
        public void Configure(EntityTypeBuilder<DeviceOtp> builder)
        {
            builder.ToTable("device_otps");

            // ===== Key =====
            builder.HasKey(x => x.Id);

            // ===== Properties =====
            builder.Property(x => x.UserDeviceId)
                   .IsRequired();

            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.ExpiredAt)
                   .IsRequired();

            builder.Property(x => x.IsUsed)
                   .IsRequired();

            // ===== Indexes =====
            builder.HasIndex(x => x.UserDeviceId);

            builder.HasIndex(x => new { x.UserDeviceId, x.IsUsed, x.ExpiredAt });
            builder.HasOne<UserDevice.Domain.Entities.UserDevice>()
                   .WithMany()
                   .HasForeignKey(x => x.UserDeviceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
