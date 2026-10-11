using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Security;
using Auth.Application.Common.Errors;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public class ForgotPasswordCommandHandler(IPasswordResetTokenRepository _tokenRepository,
        IPasswordResetTokenGenerator _resetTokenGenerator,
        IUnitOfWork _unitOfWork,
        IUserRepository _userRepository
        ) : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null) {
                return Result<string>.Failure(new Error("UserNotFound", "User with the provided email does not exist."));
            }
            if (!user.IsActive) {
                return Result<string>.Failure(new Error("UserInactive", "User account is inactive."));
            }
            await _tokenRepository.RevokeActiveTokensAsync(user.UserId,DateTime.UtcNow,cancellationToken);
            var rawToken = _resetTokenGenerator.GenerateToken();
            var tokenHash = _resetTokenGenerator.HashToken(rawToken);
            var expiration = DateTime.UtcNow.AddMinutes(15);
            var resetToken = PasswordResetToken.Create(user.UserId, tokenHash, DateTime.UtcNow, expiration);
            await _tokenRepository.AddAsync(resetToken, cancellationToken);
            user.RequestPasswordReset(rawToken, expiration);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Success("Success");
        }
    }
}
