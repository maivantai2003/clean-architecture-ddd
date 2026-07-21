using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using Search.Application.Abstractions.Persistence;
using Search.Application.Outbox;
using Search.Infrastructure.Elasticsearch;
using Search.Infrastructure.Inbox;
using Search.Infrastructure.Messaging.Configuration;
using Search.Infrastructure.Messaging.Idempotent;
using Search.Infrastructure.Outbox;
using Search.Infrastructure.Persistence;
namespace Search.Infrastructure.Config
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<UserDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //
            services.Configure<ElasticOptions>(configuration.GetSection("Elastic"));
            //
            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<ElasticOptions>>().Value
            );
            services.AddSingleton<IElasticClient>(sp =>
            {
                //var options = sp.GetRequiredService<ElasticOptions>();

                //var settings = new ConnectionSettings(new Uri(options.Url))
                //    .DefaultIndex(options.UserIndex)
                //    .RequestTimeout(TimeSpan.FromSeconds(5));

                //return new ElasticClient(settings);
                var options = sp.GetRequiredService<IOptions<ElasticOptions>>().Value;

                var settings = new ConnectionSettings(new Uri(options.Url))
                    .DefaultIndex(options.UserIndex)
                    .RequestTimeout(TimeSpan.FromSeconds(5));

                return new ElasticClient(settings);
            });
            //
            services.AddScoped<IElasticInitializer, ElasticInitializer>();
            //
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<IInboxMessageRepository, InboxMessageRepository>();
            services.AddScoped<IUserSearchService, ElasticUserSearchService>();
            services.AddScoped<IIdempotentExecutor, InboxConsumerExecutor>();
            services.AddMessagingConfig(configuration);
            return services;
        }
    }
}
