using Prometheus;

namespace APIGateway.Middlewares
{
    public class GatewayMetricsMiddleware
    {
        private static readonly Counter RequestCounter =
        Metrics.CreateCounter(
            "gateway_requests_total",
            "Total requests through gateway",
            new CounterConfiguration
            {
                LabelNames = new[] { "method", "path", "status" }
            });

        private readonly RequestDelegate _next;

        public GatewayMetricsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            RequestCounter.WithLabels(
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode.ToString()
            ).Inc();
        }
    }
}
