//using Newtonsoft.Json;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using User.Domain.Common;
//namespace User.Infrastructure.Outbox
//{
//    public class OutboxMessage
//    {
//        public Guid Id { get; private set; }
//        public string MessageId { get; private set; } = default!;
//        public DateTime OccurredOn { get; private set; }
//        public string Type { get; private set; } = default!;
//        public string Content { get; private set; } = default!;
//        public DateTime? ProcessedOn { get; private set; }
//        public DateTime? ProcessingStartedOn { get; private set; }
//        public string? Error { get; private set; }
//        public int RetryCount { get; private set; } = 0;
//        public OutboxStatus Status { get; private set; } = OutboxStatus.Pending;
//        [Timestamp]
//        public byte[] RowVersion { get; private set; } = default!;

//        public OutboxMessage() { }

//        public OutboxMessage(DomainEvent domainEvent)
//        {
//            Id = Guid.NewGuid();
//            MessageId = Guid.NewGuid().ToString();
//            OccurredOn = domainEvent.OccurredOn;
//            Type = domainEvent.GetType().AssemblyQualifiedName!;
//            Content = JsonConvert.SerializeObject(domainEvent);
//            Status = OutboxStatus.Pending;
//            RetryCount = 0;
//        }

//        public void MarkProcessing()
//        {
//            Status = OutboxStatus.Processing;
//            ProcessingStartedOn = DateTime.UtcNow;
//        }
//        public void MarkCompleted()
//        {
//            Status = OutboxStatus.Completed;
//            ProcessedOn = DateTime.UtcNow;
//            Error= null;
//        }
//        public void MarkFailed(string error)
//        {
//            Status = OutboxStatus.Failed;
//            RetryCount++;
//            Error = error;
//        }
//        public bool CanRetry(int maxRetry)
//            => RetryCount < maxRetry && Status == OutboxStatus.Failed;
//        public enum OutboxStatus
//        {
//            Pending = 0,
//            Processing = 1,
//            Failed = 2,
//            Completed = 3
//        }
//    }
//}
