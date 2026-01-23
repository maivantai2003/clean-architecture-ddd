using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserDevice.Infrastructure.Persistence.Configurations
{
    public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice.Domain.Entities.UserDevice>
    {
        public void Configure(EntityTypeBuilder<UserDevice.Domain.Entities.UserDevice> builder)
        {
            builder.ToTable("user-devices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid");

            builder.Property(x => x.UserId)
                   .IsRequired()
                   .HasColumnType("uuid");

            builder.Property(x => x.DeviceId)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnType("varchar(200)");

            builder.Property(x => x.DeviceType)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.Location)
                   .HasMaxLength(255)
                   .HasColumnType("varchar(255)");

            builder.Property(x => x.FirebaseToken)
                   .HasMaxLength(500)
                   .HasColumnType("varchar(500)");

            builder.Property(x => x.IsTrusted)
                   .IsRequired();

            builder.Property(x => x.LastLoginAt)
                   .IsRequired()
                   .HasColumnType("timestamptz");
            builder.HasIndex(x => new { x.UserId, x.DeviceId })
                   .IsUnique()
                   .HasDatabaseName("ux_user_devices_user_device");

            builder.HasIndex(x => x.UserId)
                   .HasDatabaseName("ix_user_devices_user_id");

        }
    }
}
