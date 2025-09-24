using AcademyIO.Payments.API.Data;
using AcademyIO.WebAPI.Core.DatabaseFlavor;
using static AcademyIO.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;

namespace AcademyIO.Payments.API.Configuration
{
    public static class AddEF
    {
        public static IServiceCollection AddContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureProviderForContext<PaymentsContext>(DetectDatabase(configuration), "AcademyIO.Payments.API");

            services.AddMemoryCache()
                .AddDataProtection();

            return services;
        }
    }
}
