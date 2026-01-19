using MediatR;
using Notification.Application.Abstractions;

namespace Notification.Application.Features.Emails.Commands
{
    public class SendWelcomeEmailCommandHandler : IRequestHandler<SendWelcomeEmailCommand, Unit>
    {

        private readonly IEmailSender _emailSender;
        private readonly IWelcomeEmailTemplate _welcomeEmailTemplate;
        public SendWelcomeEmailCommandHandler(IEmailSender emailSender, IWelcomeEmailTemplate welcomeEmailTemplate)
        {
            _emailSender = emailSender;
            _welcomeEmailTemplate = welcomeEmailTemplate;
        }
        public async Task<Unit> Handle(SendWelcomeEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage =await _welcomeEmailTemplate.Create(request.Email,request.FullName);
            await _emailSender.SendEmailAsync(emailMessage.To,emailMessage.Subject,emailMessage.Body,cancellationToken);
            return Unit.Value;
        }
    }
}
