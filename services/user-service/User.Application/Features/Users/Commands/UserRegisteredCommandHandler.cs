using MediatR;
using User.Application.Abstractions.Persistence;
using User.Application.Common.Errors;

namespace User.Application.Features.Users.Commands
{
    public class UserRegisteredCommandHandler : IRequestHandler<UserRegisterCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserRegisteredCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.GetByIdAsync(request.UserId) is not null)
            {
                return Result<Guid>.Failure(UserErrors.EmailAlreadyExists);
            }
            var user = new User.Domain.Entities.User(
                request.UserId,
                request.Name,
                request.Email,
                request.Name.ToLower().Replace(" ", ".")
                );
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(user.UserId);
        }
    }
}
