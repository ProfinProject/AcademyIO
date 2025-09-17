using AcademyIO.Courses.API.Data;
using AcademyIO.WebAPI.Core.DatabaseFlavor;
using static AcademyIO.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;

namespace AcademyIO.Courses.API.Configuration
{
    public static class AddEF
    {
        public static IServiceCollection AddContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureProviderForContext<CoursesContext>(DetectDatabase(configuration), "AcademyIO.Courses.API");

            services.AddMemoryCache()
                .AddDataProtection();

            return services;
        }
    }
}
