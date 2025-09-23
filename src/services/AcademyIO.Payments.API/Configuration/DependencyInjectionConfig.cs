using AcademyIO.Payments.API.AntiCorruption;
using AcademyIO.Payments.API.Business;
using AcademyIO.Payments.API.Data;
using AcademyIO.Payments.API.Data.Repository;
using AcademyIO.WebAPI.Core.User;

namespace AcademyIO.Payments.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentCreditCardFacade, PaymentCreditCardFacade>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<PaymentsContext>();
        }
    }
}