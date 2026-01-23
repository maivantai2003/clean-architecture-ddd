using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Application.Interfaces;
using Realtime.Infrastructure.Messaging.Configuration;
using Realtime.Infrastructure.SignalR;
using Realtime.Infrastructure.Stores;

namespace Realtime.Infrastructure.Config
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessagingConfig(configuration);
            services.AddScoped<IUserConnectionStore, RedisUserConnectionStore>();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddScoped<IRealtimePublisher, SignalRPublisher>();
            return services;
        }
    }
}
