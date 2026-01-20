using System.Diagnostics;

namespace APIGateway.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var activity = Activity.Current ?? new Activity("GatewayRequest");
            activity.Start();

            context.Response.Headers["X-Correlation-Id"] = activity.Id!;

            await _next(context);

            activity.Stop();
        }
    }
}
