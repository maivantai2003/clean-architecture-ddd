using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace Search.Application.Config
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
            return services;
        }
    }
}
