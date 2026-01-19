using User.Application.DTOs;

namespace User.Application.Abstractions.Persistence
{
    public interface IAuthGrpcClient
    {
        Task<UserDTO> GetUser(Guid userId);
    }
}
