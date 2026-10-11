using MediatR;
using Notification.Application.Abstractions;

namespace Notification.Application.Features.Emails.Commands
{
    public class SendPasswordResetEmailCommandHandler(IEmailSender _emailSender, IPasswordResetEmailTemplate _passwordResetEmailTemplate) : IRequestHandler<SendPasswordResetEmailCommand, Unit>
    {
        public async Task<Unit> Handle(SendPasswordResetEmailCommand request, CancellationToken cancellationToken)
        {
            var resetLink =
                $"{"http://localhost:3000/reset-password"}" +
                $"?token={Uri.EscapeDataString(request.ResetToken)}";

            var emailMessage =await _passwordResetEmailTemplate.Create(request.Email,resetLink,request.ExpiresAtUtc);

            await _emailSender.SendEmailAsync(emailMessage.To, emailMessage.Subject,emailMessage.Body,cancellationToken);
            return Unit.Value;
        }
    }
}
