using MediatR;
using User.Application.Abstractions.Persistence;

namespace User.Application.Authorization
{
    public abstract record SecuredRequest<TResponse> : IRequest<TResponse>, IRequirePermission
    {
        public abstract Permission Permission { get; }
    }
}
