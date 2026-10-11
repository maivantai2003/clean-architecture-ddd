using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Abstractions.Persistence;
using User.Application.Authorization;
using User.Application.Security;
using User.Infrastructure.BackgroundJobs;
using User.Infrastructure.Caching;
using User.Infrastructure.GrpcClients;
using User.Infrastructure.Inbox;
using User.Infrastructure.Messaging;
using User.Infrastructure.Messaging.Configuration;
using User.Infrastructure.Messaging.Idempotent;
using User.Infrastructure.Messaging.Kafka;
using User.Infrastructure.Persistence;
using User.Infrastructure.Persistence.Repositories;
using User.Infrastructure.Security;

namespace User.Infrastructure.Config
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<UserDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddMessagingConfig(configuration);
            services.AddKafkaConfig(configuration);
            services.AddHostedService<OutboxProcessor>();
            services.AddScoped<IDomainEventMapper, DomainEventMapper>();
            services.AddScoped<IInboxMessageRepository, InboxMessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthGrpcClient, AuthGrpcClient>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IPermissionChecker, CachedPermissionChecker>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IIdempotentExecutor,InboxConsumerExecutor>();
            return services;
        }
    }
}
