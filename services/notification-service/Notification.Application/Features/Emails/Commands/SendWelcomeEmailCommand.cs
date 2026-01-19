using MediatR;

namespace Notification.Application.Features.Emails.Commands
{
    public record SendWelcomeEmailCommand(string Email,string FullName):IRequest<Unit>;
}
