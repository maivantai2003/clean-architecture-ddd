using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Abstractions.Behavior;
namespace User.Application.Config
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationConfig(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
