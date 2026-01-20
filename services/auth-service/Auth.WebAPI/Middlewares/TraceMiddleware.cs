using System.Diagnostics;
namespace Auth.WebAPI.Middlewares
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (Activity.Current == null &&
                context.Request.Headers.TryGetValue("X-Correlation-Id", out var traceId))
            {
                var activity = new Activity("ServiceRequest");
                activity.SetParentId(traceId!);
                activity.Start();

                await _next(context);

                activity.Stop();
                return;
            }

            await _next(context);
        }
    }
}
