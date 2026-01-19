using Notification.Application.DTOs;
namespace Notification.Application.Abstractions
{
    public interface IWelcomeEmailTemplate
    {
        Task<EmailMessageDTO> Create(string email, string fullName);
    }
}
