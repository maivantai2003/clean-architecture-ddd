using User.Application.Common;
using FluentValidation;
using MediatR;
using User.Application.Common.Errors;

namespace User.Application.Abstractions.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(v => v.Validate(context))
                .SelectMany(v => v.Errors)
                .Where(f => f != null)
                .ToList();
            if (!failures.Any())
            {
                return await next();
            }
            var errors = failures.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage)
                        .ToArray());
            return (TResponse)(object)Result.ValidationFailure(errors);
        }
    }
}
