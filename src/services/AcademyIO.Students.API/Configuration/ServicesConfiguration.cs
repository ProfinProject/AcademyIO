using AcademyIO.Core.Interfaces.Services;
using AcademyIO.Core.Notifications;
using AcademyIO.Students.API.Application.Commands;
using AcademyIO.Students.API.Application.Queries;
using AcademyIO.Students.API.Data.Repository;
using AcademyIO.Students.API.Models;
using AcademyIO.Students.API.Services;
using AcademyIO.WebAPI.Core.User;

namespace AcademyIO.Students.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IRegistrationQuery, RegistrationQuery>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddRegistrationCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddUserCommand>());
            services.AddHostedService<UserRegisteredIntegrationHandler>();
            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }
    }
}
