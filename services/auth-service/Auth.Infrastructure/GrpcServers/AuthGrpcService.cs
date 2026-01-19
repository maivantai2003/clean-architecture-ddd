using Auth.Application.Features.Users.Queries;
using Grpc.Core;
using MediatR;
using Shared.Contracts.Grpcs;
namespace Auth.Infrastructure.GrpcServers
{
    public class AuthGrpcService : AuthGrpc.AuthGrpcBase
    {

        private readonly IMediator _mediator;
        public AuthGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(new GetUserQuery(Guid.Parse(request.UserId)));
            return new GetUserResponse
            {
                UserId = response.UserId.ToString(),
                Email = response.Email,
                UserName= response.Name,
            };
        }
    }
}
