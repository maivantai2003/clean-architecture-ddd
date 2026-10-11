using MediatR;

namespace Notification.Application.Features.Emails.Commands
{
    public record SendPasswordResetEmailCommand(
        string Email,
        string ResetToken,
        DateTime ExpiresAtUtc
    ) : IRequest<Unit>;
}
