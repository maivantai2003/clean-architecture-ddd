using Microsoft.Extensions.Options;
using Notification.Application.Abstractions;
using System.Net;
using System.Net.Mail;

namespace Notification.Infrastructure.Email
{
    public class GmailEmailSender :IEmailSender
    {
        private readonly GmailOptions _gmailOptions;
        public GmailEmailSender(IOptions<GmailOptions> gmailOptions)
        {
            _gmailOptions = gmailOptions.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_gmailOptions.Username, _gmailOptions.AppPassword),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_gmailOptions.Username),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);
            await smtpClient.SendMailAsync(mailMessage, cancellationToken);
        }
    }
}
