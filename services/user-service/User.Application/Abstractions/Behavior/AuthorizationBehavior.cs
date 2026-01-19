using MediatR;
using System.Reflection;
using User.Application.Abstractions.Persistence;
using User.Application.Authorization;
using User.Application.Common.Exceptions;

namespace User.Application.Abstractions.Behavior
{
    public sealed class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IAuthorizationService _authorization;
        private readonly ICurrentUser _currentUser;
        public AuthorizationBehavior(IAuthorizationService authorization, ICurrentUser currentUser)
        {
            _authorization = authorization;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
            var permissions = request
                 .GetType()
                 .GetCustomAttributes<RequirePermissionAttribute>(true)
                 .Select(x => x.Permission)
                 .ToList();

            if (!permissions.Any())
                return await next();

            if (!_currentUser.IsAuthenticated)
                throw new UnauthorizedException("User is not authenticated");
            Console.WriteLine("Permission: "+ _currentUser.Principal.ToString());
            foreach (var permission in permissions)
            {
                await _authorization.EnsurePermissionAsync(_currentUser.Principal,permission,ct);
            }

            return await next();
        }
    }

}
