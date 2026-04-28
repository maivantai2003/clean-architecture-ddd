using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserDevice.Application.Abstractions.Behavior;
namespace UserDevice.Application.Config
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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
