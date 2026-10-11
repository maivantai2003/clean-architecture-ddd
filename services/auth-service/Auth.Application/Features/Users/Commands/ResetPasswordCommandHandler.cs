using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Security;
using Auth.Application.Common.Errors;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public class ResetPasswordCommandHandler(
         IPasswordResetTokenRepository _tokenRepository,
         IUserRepository _userRepository,
         IPasswordResetTokenGenerator _tokenGenerator,
         IPasswordHasher _passwordHasher,
         IUnitOfWork _unitOfWork) : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var tokenHash =_tokenGenerator.HashToken(request.Token);

            var resetToken =await _tokenRepository.GetByTokenHashAsync(tokenHash,cancellationToken);

            //if (resetToken is null)
            //    return Result<string>.Failure(
            //        new InvalidPasswordResetTokenError());

            var now = DateTime.UtcNow;

            //if (resetToken.IsUsed)
            //    throw new InvalidPasswordResetTokenException();

            //if (resetToken.IsExpired(now))
            //    throw new InvalidPasswordResetTokenException();

            var user = await _userRepository.GetByIdAsync(resetToken.UserId);

            //if (user is null)
            //    return 

            //if (!user.IsActive)
            //    throw new InvalidPasswordResetTokenException();

            var newPasswordHash =_passwordHasher.Hash(request.NewPassword);

            user.ChangePassword(newPasswordHash);

            resetToken.MarkAsUsed(now);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
            return Result<string>.Success("Password reset successfully.");
        }
    }
}
