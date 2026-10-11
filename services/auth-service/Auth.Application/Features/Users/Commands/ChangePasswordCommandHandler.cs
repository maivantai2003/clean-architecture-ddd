using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Security;
using Auth.Application.Common.Errors;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public class ChangePasswordCommandHandler(IUserRepository _userRepository,IPasswordHasher _passwordHasher, IUnitOfWork _unitOfWork) : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user= await _userRepository.GetByIdAsync(request.UserId);
            if(user is null)
            {
                return Result<string>.Failure(new Error("UserNotFound", "User not found."));
            }
            var isValid=_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash);
            if(!isValid)
            {
                return Result<string>.Failure(new Error("InvalidCurrentPassword", "The current password is incorrect."));
            }
            var newPasswordHash=_passwordHasher.Hash(request.NewPassword);
            user.ChangePassword(newPasswordHash);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Success("Password changed successfully.");
        }
    }
}
