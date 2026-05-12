using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Security;
using Auth.Application.Common.Errors;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public class CreateUserCommandHandler:IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork,IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.GetByEmailAsync(request.Email) is not null)
            {
                return Result<Guid>.Failure(EmailErrors.EmailAlreadyInUse);
            }
            var hash = _passwordHasher.Hash(request.Password);
            var user = new User(request.userName,request.Email,hash);
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(user.UserId);
        }
    }
}
