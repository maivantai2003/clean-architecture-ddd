using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace User.Application.Abstractions.Behavior
{
    public sealed class LoggingBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken){
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation(
                "Handling request {RequestName} {@Request}",
                requestName,
                request);

            var sw = Stopwatch.StartNew();
            try
            {
                var response = await next();
                sw.Stop();
                _logger.LogInformation(
                    "Completed request {RequestName} in {ElapsedMilliseconds} ms",
                    requestName,
                    sw.ElapsedMilliseconds);
                return response;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(
                    ex,
                    "Request {RequestName} failed after {ElapsedMilliseconds} ms",
                    requestName,
                    sw.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
