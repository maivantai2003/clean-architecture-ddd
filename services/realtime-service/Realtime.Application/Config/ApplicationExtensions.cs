using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
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
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
