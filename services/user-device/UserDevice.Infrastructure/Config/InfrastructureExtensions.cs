using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserDevice.Application.Abstractions.Persistence;
using UserDevice.Infrastructure.Persistence;
using UserDevice.Infrastructure.Persistence.Repositories;

namespace UserDevice.Infrastructure.Config
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<UserDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserDeviceRepository, UserDeviceRepository>();
            services.AddScoped<IDeviceOtpRepository, DeviceOtpRepository>();
            return services;
        }
    }
}
