using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Search.Application.Abstractions.Persistence;
using Search.Infrastructure.Outbox;
using Search.Infrastructure.Persistence;
namespace Search.Infrastructure.BackgroundJobs
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxProcessor> _logger;
        public OutboxProcessor(IServiceProvider serviceProvider, ILogger<OutboxProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const int BATCH_SIZE = 10;

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SearchDbContext>();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
                var mapper = scope.ServiceProvider.GetRequiredService<IDomainEventMapper>();

                var messages = await db.OutboxMessages
                    .Where(x => x.Status == OutboxMessage.OutboxStatus.Pending)
                    .OrderBy(x => x.OccurredOn)
                    .Take(BATCH_SIZE)
                    .ToListAsync(stoppingToken);

                foreach (var msg in messages)
                {
                    try
                    {
                        msg.MarkProcessing();

                        var domainEvent = JsonConvert.DeserializeObject(
                            msg.Content,
                            Type.GetType(msg.Type)!
                        );

                        var integrationEvent = mapper.Map(domainEvent!);

                        await publisher.Publish(integrationEvent, ctx =>
                        {
                            ctx.MessageId = Guid.Parse(msg.MessageId);
                            ctx.CorrelationId = Guid.Parse(msg.MessageId);
                        });
                        Console.WriteLine($"Published event {msg.Type} with MessageId {msg.MessageId}");
                        Console.WriteLine($"Event content: {msg.Content}");
                        msg.MarkCompleted();
                    }
                    catch (Exception ex)
                    {
                        msg.MarkFailed(ex.Message);
                        _logger.LogError(ex, "Publish failed");
                    }
                }

                await db.SaveChangesAsync(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
