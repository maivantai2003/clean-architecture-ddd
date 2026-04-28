using Search.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Search.Infrastructure.BackgroundJobs
{
    public class InboxCleanupJob:BackgroundService
    {
        private readonly ILogger<InboxCleanupJob> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public InboxCleanupJob(ILogger<InboxCleanupJob> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromHours(6), stoppingToken);
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<SearchDbContext>();
                    var cutoff = DateTime.UtcNow.AddDays(-7);

                    var deleted = await db.InboxMessages.Where(x => x.ProcessedAt < cutoff).ToListAsync();
                    db.InboxMessages.RemoveRange(deleted);
                    await db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation(
                        "Inbox cleanup deleted {Count} messages", deleted.Count);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Inbox cleanup failed");
                }
            }
        }
    }
}
