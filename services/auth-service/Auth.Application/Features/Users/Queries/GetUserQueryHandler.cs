using Auth.Application.Abstractions.Persistence;
using Auth.Application.DTOs;
using MediatR;

namespace Auth.Application.Features.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByIdAsync(request.userId);
            return new UserDTO
            {
                Email=result.Email,
                Name=result.Username,
                UserId=result.UserId
            };
        }
    }
}
