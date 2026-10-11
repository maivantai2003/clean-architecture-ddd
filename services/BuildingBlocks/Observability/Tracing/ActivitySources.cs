using System.Diagnostics;

namespace Observability.Tracing
{
    public static class ActivitySources
    {
        public const string ServiceName = "Microservices";
        public static readonly ActivitySource Default = new(ServiceName);
    }
}
