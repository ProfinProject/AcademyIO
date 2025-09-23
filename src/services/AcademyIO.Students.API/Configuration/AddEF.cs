using AcademyIO.Students.API.Data;
using AcademyIO.WebAPI.Core.DatabaseFlavor;
using static AcademyIO.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;

namespace AcademyIO.Students.API.Configuration
{
    public static class AddEF
    {
        public static IServiceCollection AddContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureProviderForContext<StudentsContext>(DetectDatabase(configuration), "AcademyIO.Students.API");

            services.AddMemoryCache()
                .AddDataProtection();

            return services;
        }
    }
}
