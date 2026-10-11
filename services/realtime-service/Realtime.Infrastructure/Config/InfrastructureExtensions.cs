using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Application.Interfaces;
using Realtime.Infrastructure.Messaging.Configuration;
using Realtime.Infrastructure.SignalR;
using Realtime.Infrastructure.Stores;
using StackExchange.Redis;
namespace Realtime.Infrastructure.Config
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessagingConfig(configuration);
            var redisConnection =configuration["Redis:ConnectionString"]!;

            services.AddSignalR().AddStackExchangeRedis(redisConnection,
                    options =>
                    {
                        options.Configuration.ChannelPrefix ="Realtime";
                    });

            services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(
                    redisConnection));

            services.AddScoped<IUserConnectionStore, RedisUserConnectionStore>();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddScoped<IRealtimePublisher, SignalRPublisher>();
            return services;
        }
    }
}
