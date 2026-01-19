namespace Notification.Infrastructure.Email
{
    public class EmailMessage
    {
        public string To { get; }
        public string Subject { get; }
        public string Body { get; }

        public EmailMessage(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
