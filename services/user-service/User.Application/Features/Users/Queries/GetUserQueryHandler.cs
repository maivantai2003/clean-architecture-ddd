using MediatR;
using User.Application.Abstractions.Persistence;
using User.Application.DTOs;

namespace User.Application.Features.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IAuthGrpcClient _authGrpcClient;
        public GetUserQueryHandler(IAuthGrpcClient authGrpcClient)
        {
            _authGrpcClient = authGrpcClient;
        }
        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authGrpcClient.GetUser(request.userId);
            return new UserDTO() { DateOfBirth=result.DateOfBirth,
            Email=result.Email, FirstName=result.FirstName,LastName=result.LastName,Id=result.Id};
        }
    }
}
