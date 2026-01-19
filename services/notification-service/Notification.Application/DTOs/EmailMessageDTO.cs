namespace Notification.Application.DTOs
{
    public class EmailMessageDTO
    {
        public string To { get; }
        public string Subject { get; }
        public string Body { get; }

        public EmailMessageDTO(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
