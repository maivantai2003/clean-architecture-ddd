using MediatR;
using User.Application.Abstractions.Persistence;
using User.Domain.Entities;

namespace User.Application.Features.Users.Commands
{
    public class UserRegisteredCommandHandler : IRequestHandler<UserRegisterCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserRegisteredCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.GetByIdAsync(request.UserId) is not null)
            {
                throw new InvalidOperationException("User already exists.");
            }
            var user = new User.Domain.Entities.User(
                request.UserId,
                request.Name,
                request.Email,
                request.Name.ToLower().Replace(" ", ".")
                );
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user.UserId;
        }
    }
}
