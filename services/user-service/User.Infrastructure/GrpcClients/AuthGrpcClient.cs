using Shared.Contracts.Grpcs;
using User.Application.Abstractions.Persistence;
using User.Application.DTOs;

namespace User.Infrastructure.GrpcClients
{
    public class AuthGrpcClient : IAuthGrpcClient
    {
        private readonly AuthGrpc.AuthGrpcClient _client;
        public AuthGrpcClient(AuthGrpc.AuthGrpcClient client)
        {
            _client = client;
        }
        public async Task<UserDTO> GetUser(Guid userId)
        {
            var response = await _client.GetUserAsync(new GetUserRequest{ UserId=userId.ToString()});
            return new UserDTO() { DateOfBirth=DateTime.UtcNow, 
                Email=response.Email,
            FirstName=response.UserName,
            Id=userId,
            LastName=response.UserName};
        }
    }
}
