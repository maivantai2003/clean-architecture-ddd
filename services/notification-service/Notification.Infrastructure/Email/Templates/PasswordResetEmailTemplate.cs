using Notification.Application.Abstractions;
using Notification.Application.DTOs;

namespace Notification.Infrastructure.Email.Templates
{
    public class PasswordResetEmailTemplate : IPasswordResetEmailTemplate
    {
        public Task<EmailMessageDTO> Create(string email, string resetLink, DateTime expiresAtUtc)
        {
            var body = $"""
                <html>
                <body>
                    <h2>Password Reset</h2>

                    <p>
                        We received a request to reset your password.
                    </p>

                    <p>
                        Click the link below to reset your password:
                    </p>

                    <p>
                        <a href="{resetLink}">
                            Reset Password
                        </a>
                    </p>

                    <p>
                        This link will expire at:
                        {expiresAtUtc:yyyy-MM-dd HH:mm:ss} UTC
                    </p>

                    <p>
                        If you did not request a password reset,
                        you can safely ignore this email.
                    </p>
                </body>
                </html>
                """;

            var message = new EmailMessageDTO(
                email,
                "Reset your password",
                body);

            return Task.FromResult(message);
        }
    }
}
