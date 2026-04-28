
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Search.Infrastructure.Persistence;
using static Search.Infrastructure.Outbox.OutboxMessage;

namespace Search.Infrastructure.BackgroundJobs
{
    public class OutboxCleanupJob : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<OutboxCleanupJob> _logger;
        public OutboxCleanupJob(IServiceScopeFactory serviceScopeFactory,ILogger<OutboxCleanupJob> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SearchDbContext>();
                var cutoff = DateTime.UtcNow.AddDays(-7);
                var messages = await db.OutboxMessages.Where(x => x.Status == OutboxStatus.Completed &&
                x.ProcessedOn < cutoff).ToListAsync(stoppingToken);
                db.OutboxMessages.RemoveRange(messages);
                await db.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Deleted {Count} outbox messages", messages.Count());
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
