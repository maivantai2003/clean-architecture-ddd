using Auth.Application.Common.Errors;
using Auth.Domain.Exceptions;
using MediatR;

namespace Auth.Application.Abstractions.Behavior{
    public sealed class DomainExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : notnull
    {
        public async Task<Result<TResponse>> Handle(
            TRequest request,
            RequestHandlerDelegate<Result<TResponse>> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (DomainException ex)
            {
                var error = new Application.Common.Errors.Error(ex.Error.Code,ex.Error.Message);

                return Result<TResponse>.Failure(error);
            }
        }
    }
}
