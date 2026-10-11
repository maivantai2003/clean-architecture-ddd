using Auth.Application.Abstractions.Caching;
using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Security;
using Auth.Infrastructure.BackgroundJobs;
using Auth.Infrastructure.Caching;
using Auth.Infrastructure.Inbox;
using Auth.Infrastructure.Messaging;
using Auth.Infrastructure.Messaging.Configuration;
using Auth.Infrastructure.Messaging.Idempotent;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Persistence.Identity;
using Auth.Infrastructure.Persistence.Repositories;
using Auth.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Auth.Infrastructure.Config
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAppActionRepository, AppActionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMessagingConfig(configuration);
            services.AddHostedService<OutboxProcessor>();
            services.AddScoped<IInboxMessageRepository, InboxMessageRepository>();
            services.AddScoped<IDomainEventMapper, DomainEventMapper>();
            services.AddScoped<IIdempotentExecutor,InboxConsumerExecutor>();
            //services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
            //services.AddSingleton<IRabbitMQChannelFactory, RabbitMQChannelFactory>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IPermissionRepository, PermissionsRepository>();
            services.AddScoped<IPermissionChecker, PermissionChecker>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddSingleton<ITokenLifetimeProvider, TokenLifetimeProvider>();
            services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
            services.AddScoped<IPasswordResetTokenGenerator, PasswordResetTokenGenerator>();
            return services;
        }
    }
}
