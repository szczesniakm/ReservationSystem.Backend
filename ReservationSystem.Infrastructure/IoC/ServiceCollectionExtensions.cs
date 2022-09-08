using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.Domain.Repositories;
using ReservationSystem.Infrastructure.Jwt;
using ReservationSystem.Infrastructure.Repositories;
using ReservationSystem.Infrastructure.Settings;

namespace ReservationSystem.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<LdapSettings>(configuration.GetSection("LdapSettings"));
            services.Configure<AmtSettings>(configuration.GetSection("AmtSettings"));
            services.AddDbContext<ReservationSystemContext>();
            services.AddTransient<IHostRepository, HostRepository>();
            services.AddTransient<IOSRepository, OSRepository>();
            services.AddTransient<IReservationLogRepository, ReservationLogRepository>();
            services.AddTransient<JwtTokenService>();
            services.AddTransient<LdapAuthenticationProvider>();

            return services;
        }
    }
}
