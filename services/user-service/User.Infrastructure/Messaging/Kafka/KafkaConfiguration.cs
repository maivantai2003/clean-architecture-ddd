using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace User.Infrastructure.Messaging.Kafka
{
    public static class KafkaConfiguration
    {
        public static IServiceCollection AddKafkaConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var bootstrapServers =configuration["Kafka:BootstrapServers"]
             ?? throw new InvalidOperationException("Kafka:BootstrapServers is not configured.");
            services.AddSingleton<IProducer<string, string>>(_ =>
            {
                var producerConfig = new ProducerConfig
                {
                    BootstrapServers = bootstrapServers,
                    Acks = Acks.All,
                    EnableIdempotence = true,
                    MessageTimeoutMs = 30000,
                    MessageSendMaxRetries = 5,
                    RetryBackoffMs = 1000
                };

                return new ProducerBuilder<string, string>(producerConfig).Build();
            });
            services.AddSingleton<IConsumer<string, string>>(_ =>
            {
                var consumerConfig = new ConsumerConfig
                {
                    BootstrapServers = bootstrapServers,
                    GroupId =
                        configuration["Kafka:GroupId"] ?? "user-service",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false,
                    SessionTimeoutMs = 45000,
                    HeartbeatIntervalMs = 15000,
                    MaxPollIntervalMs = 300000
                };

                return new ConsumerBuilder<string, string>(consumerConfig).Build();
            });
            return services;
        }
    }
}
