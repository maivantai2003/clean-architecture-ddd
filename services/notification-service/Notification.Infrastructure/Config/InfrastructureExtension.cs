using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Abstractions;
using Notification.Infrastructure.Email;
using Notification.Infrastructure.Email.Templates;
using Notification.Infrastructure.Inbox;
using Notification.Infrastructure.Messaging.Idempotent;

namespace Notification.Infrastructure.Config
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services)
        {
            //services.AddRazorPages();
            services.AddScoped<IEmailSender, GmailEmailSender>();
            services.AddScoped<IWelcomeEmailTemplate, WelcomeEmailTemplate>();
            services.AddScoped<IIdempotentExecutor, InboxConsumerExecutor>();
            services.AddScoped<IInboxMessageRepository, InboxMessageRepository>();
            //services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
            return services;
        }
    }
}
