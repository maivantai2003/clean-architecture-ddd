using MediatR;
using User.Application.Abstractions.Persistence;
using User.Application.DTOs;

namespace User.Application.Features.Users.Queries
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var userDTOs = users.Select(user => new UserDTO
            {
                DateOfBirth = user.DateOfBirth ?? DateTime.MinValue,
                Email =  user.Email,
                FirstName = user.FullName,
                Id = user.UserId,
                LastName = user.FullName
            }).ToList();
            return userDTOs;
        }
    }
}
