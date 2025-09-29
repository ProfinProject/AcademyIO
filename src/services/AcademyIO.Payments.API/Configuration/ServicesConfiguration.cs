using AcademyIO.Core.Interfaces.Services;
using AcademyIO.Core.Messages.IntegrationCommands;
using AcademyIO.Core.Notifications;
using AcademyIO.Payments.API.Application.Query;
using AcademyIO.Payments.API.Business;
using AcademyIO.Payments.API.Data.Repository;
using AcademyIO.WebAPI.Core.User;

namespace AcademyIO.Payments.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IPaymentQuery, PaymentQuery>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MakePaymentCourseCommand>());

            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }
    }
}
