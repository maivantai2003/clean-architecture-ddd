using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Network;
namespace Observability
{
    public static class ObservabilityExtensions
    {
        public static WebApplicationBuilder AddObservability(this WebApplicationBuilder builder)
        {
            var serviceName =
                Environment.GetEnvironmentVariable("SERVICE_NAME")
                ?? builder.Configuration["ServiceInfo:Name"]
                ?? "UnknownService";

            var environment =
                builder.Environment.EnvironmentName;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()

                .Enrich.WithProperty(
                    "Service",
                    serviceName)

                .Enrich.WithProperty(
                    "Environment",
                    environment)

                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.TCPSink("tcp://logstash:5000").CreateLogger();

            builder.Host.UseSerilog();
            builder.Services.AddOpenTelemetryObservability(builder.Configuration,serviceName);
            return builder;
        }
    }
}
