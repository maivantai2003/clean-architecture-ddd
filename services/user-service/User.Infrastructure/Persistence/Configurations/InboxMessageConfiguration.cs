using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Infrastructure.Inbox;

namespace User.Infrastructure.Persistence.Configurations
{
    public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
    {
        public void Configure(EntityTypeBuilder<InboxMessage> builder)
        {
            builder.ToTable("inbox_messages");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.MessageId, x.Consumer })
             .IsUnique();
        }
    }
}
