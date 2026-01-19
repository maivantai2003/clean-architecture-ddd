using Auth.Application.DTOs;
using MediatR;

namespace Auth.Application.Features.Authorization.Commands
{
    public record RefreshTokenCommand(string RefreshToken):IRequest<TokenResponse>;

}
