using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realtime.Infrastructure.Messaging.Consumers;

namespace Realtime.Infrastructure.Messaging.Configuration
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddMessagingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(UserCreatedConsumer).Assembly);
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(ctx);

                });
            });

            return services;
        }
    }
}
