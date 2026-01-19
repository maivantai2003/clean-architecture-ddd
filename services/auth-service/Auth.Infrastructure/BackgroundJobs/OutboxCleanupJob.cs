using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static Auth.Infrastructure.Outbox.OutboxMessage;

namespace Auth.Infrastructure.BackgroundJobs
{
    public class OutboxCleanupJob : BackgroundService
    {
        private readonly ILogger<OutboxCleanupJob> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public OutboxCleanupJob(ILogger<OutboxCleanupJob> logger,IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

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
