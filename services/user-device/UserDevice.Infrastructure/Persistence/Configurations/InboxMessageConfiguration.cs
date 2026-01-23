using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDevice.Infrastructure.Inbox;

namespace UserDevice.Infrastructure.Persistence.Configurations
{
    public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder.ToTable("inbox_messages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnType("uuid");

            builder.Property(x => x.MessageId)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnType("varchar(200)");

            builder.Property(x => x.Consumer)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnType("varchar(200)");

            builder.Property(x => x.ProcessedAt)
                   .IsRequired()
                   .HasColumnType("timestamptz");

            // ===== Indexes =====
            builder.HasIndex(x => new { x.MessageId, x.Consumer })
                   .IsUnique()
                   .HasDatabaseName("ux_inbox_message_consumer");
        }
    }
}
