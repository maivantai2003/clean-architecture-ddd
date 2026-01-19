using Notification.Domain.ValueObjects;

namespace Notification.Domain.Entities
{
    public class Notification
    {
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public NotificationChannel Channel { get; private set; }
        public NotificationStatus Status { get; private set; }

        public void MarkAsSent()
            => Status = NotificationStatus.Sent;

        public void MarkAsFailed()
            => Status = NotificationStatus.Failed;
    }
}
