using Notification.Application.Abstractions;
using Notification.Application.DTOs;

namespace Notification.Infrastructure.Email.Templates
{
    public sealed class WelcomeEmailTemplate : IWelcomeEmailTemplate
    {
        
        public async Task<EmailMessageDTO> Create(string to, string fullName)
        {
            var subject = "Welcome 🎉";
            var body = $@" <div style='font-family: Arial, sans-serif; color: #333;'> <h2 style='color: #2E86C1;'>Xin chào {fullName}</h2> 
                       <p style='color: #27AE60;'>Đăng ký thành công.</p> <p style='background-color: #FDEDEC; padding: 10px; border-radius: 5px;'> Chúc bạn có trải nghiệm tuyệt vời! </p> 
                       </div>"; 
            return new EmailMessageDTO(to, subject, body);
        }
    }
}
