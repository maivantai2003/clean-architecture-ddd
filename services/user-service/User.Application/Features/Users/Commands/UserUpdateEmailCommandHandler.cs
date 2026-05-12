using MediatR;
using User.Application.Abstractions.Persistence;
using User.Application.Common.Errors;

namespace User.Application.Features.Users.Commands
{
    public class UserUpdateEmailCommandHandler : IRequestHandler<UserUpdateEmailCommand, Result<Domain.Entities.User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserUpdateEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Domain.Entities.User>> Handle(UserUpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<Domain.Entities.User>.Failure("User not found", Common.HttpStatus.NotFound);
            }
            user.UpdateEmail(request.NewEmail);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Domain.Entities.User>.Success(user, Common.HttpStatus.Ok);
        }
    }
}
