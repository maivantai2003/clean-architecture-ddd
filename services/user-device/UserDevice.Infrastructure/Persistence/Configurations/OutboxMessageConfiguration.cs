//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UserDevice.Infrastructure.Outbox;

//namespace UserDevice.Infrastructure.Persistence.Configurations
//{
//    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
//    {
//        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
//        {
//            builder.ToTable("outbox_messages");

//            // ===== Primary Key =====
//            builder.HasKey(x => x.Id);

//            builder.Property(x => x.Id)
//                   .HasColumnType("uuid");

//            builder.Property(x => x.MessageId)
//                   .IsRequired()
//                   .HasMaxLength(200)
//                   .HasColumnType("varchar(200)");

//            builder.Property(x => x.OccurredOn)
//                   .IsRequired()
//                   .HasColumnType("timestamptz");

//            builder.Property(x => x.Type)
//                   .IsRequired()
//                   .HasColumnType("text");

//            builder.Property(x => x.Content)
//                   .IsRequired()
//                   .HasColumnType("jsonb");

//            builder.Property(x => x.ProcessedOn)
//                   .HasColumnType("timestamptz");

//            builder.Property(x => x.ProcessingStartedOn)
//                   .HasColumnType("timestamptz");

//            builder.Property(x => x.Error)
//                   .HasColumnType("text");

//            builder.Property(x => x.RetryCount)
//                   .IsRequired();

//            builder.Property(x => x.Status)
//                   .IsRequired()
//                   .HasConversion<int>();

//            // ===== Optimistic Concurrency (Postgres way) =====
//            builder.Property(x => x.RowVersion)
//                   .IsRowVersion()
//                   .HasColumnName("xmin");

//            // ===== Indexes =====
//            builder.HasIndex(x => x.Status)
//                   .HasDatabaseName("ix_outbox_status");

//            builder.HasIndex(x => new { x.Status, x.OccurredOn })
//                   .HasDatabaseName("ix_outbox_status_occurred");

//            builder.HasIndex(x => x.MessageId)
//                   .IsUnique()
//                   .HasDatabaseName("ux_outbox_message_id");
//        }
//    }
//}
