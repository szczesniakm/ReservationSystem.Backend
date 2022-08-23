using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ReservationSystem.Application.Models;
using ReservationSystem.Application.Services;

namespace ReservationSystem.Application.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<OSsService>();
            services.AddTransient<HostsService>();
            services.AddTransient<ReservationsService>();
            services.AddTransient<AuthenticationService>();
            services.AddTransient<SchedulerService>();

            services.Configure<QuartzOptions>(options =>
            {
                options.SchedulerId = "ReservationsScheduler";
            });

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddValidatorsFromAssemblyContaining<GetAvaliableHostsRequest>();
            return services;
        }
    }
}
