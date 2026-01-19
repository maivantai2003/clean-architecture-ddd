//using Notification.Infrastructure.Messaging.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Messaging.Consumers;
namespace Notification.Infrastructure.Messaging.Configuration
{
    public static class MessagingExtensions
    {
        public static IServiceCollection AddMessagingConfig(this IServiceCollection services)
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
