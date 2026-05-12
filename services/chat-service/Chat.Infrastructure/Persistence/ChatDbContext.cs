using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Chat.Domain.Common;
using Chat.Domain.Entities;

namespace Chat.Infrastructure.Persistence
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore(typeof(DomainEvent));

            var participantsConverter = new ValueConverter<List<Guid>, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<Guid>>(v) ?? new List<Guid>());

            var participantsComparer = new ValueComparer<List<Guid>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            modelBuilder.Entity<Chat>(builder =>
            {
                builder.HasKey(x => x.ChatId);
                builder.Property(x => x.ParticipantIds)
                    .HasConversion(participantsConverter)
                    .Metadata.SetValueComparer(participantsComparer);
                builder.OwnsMany(x => x.Messages, owned =>
                {
                    owned.WithOwner().HasForeignKey("ChatId");
                    owned.Property<Guid>("MessageId");
                    owned.HasKey("MessageId");
                    owned.Property(x => x.SenderId).IsRequired();
                    owned.Property(x => x.Content).IsRequired();
                    owned.Property(x => x.IsBot).IsRequired();
                    owned.Property(x => x.SentAt).IsRequired();
                });
            });

            modelBuilder.Entity<OutboxMessage>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Type).IsRequired();
                builder.Property(x => x.Content).IsRequired();
                builder.Property(x => x.OccurredOn).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
