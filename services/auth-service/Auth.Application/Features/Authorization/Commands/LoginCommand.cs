using Auth.Application.Common.Errors;
using Auth.Application.DTOs;
using MediatR;

namespace Auth.Application.Features.Authorization.Commands
{
    public record LoginCommand(string Email, string Password):IRequest<Result<TokenResponse>>;
}
