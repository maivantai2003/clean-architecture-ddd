using Notification.Application.DTOs;

namespace Notification.Application.Abstractions
{
    public interface IPasswordResetEmailTemplate
    {
        Task<EmailMessageDTO> Create(
            string email,string resetLink,DateTime expiresAtUtc);
    }
}
