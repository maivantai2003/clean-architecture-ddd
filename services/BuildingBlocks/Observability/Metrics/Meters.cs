using System.Diagnostics.Metrics;

namespace Observability.Metrics
{
    public static class Meters
    {
        public const string ServiceName = "Microservices";

        public static readonly Meter Default =
            new(ServiceName);
    }
}
