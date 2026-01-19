using Auth.Application.Abstractions.Persistence;
using Auth.Application.DTOs;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public class UserUpdateEmailCommandHandler : IRequestHandler<UserUpdateEmailCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserUpdateEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<User>> Handle(UserUpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var user=await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<User>.Failure("User not found", Common.HttpStatus.NotFound);
            }
            user.UpdateEmail(request.NewEmail);
            await _unitOfWork.SaveInternalAsync(cancellationToken);
            return Result<User>.Success(user, Common.HttpStatus.Ok);
        }
    }
}
