using AcademyIO.Auth.API.Data;
using AcademyIO.WebAPI.Core.DatabaseFlavor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static AcademyIO.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;


namespace AcademyIO.Auth.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureProviderForContext<ApplicationDbContext>(DetectDatabase(configuration), "AcademyIO.Auth.API");

            services.AddMemoryCache()
                .AddDataProtection();

            services.AddDefaultIdentity<IdentityUser<Guid>>()
             .AddRoles<IdentityRole<Guid>>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddSignInManager()
             .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
             .AddDefaultTokenProviders();            

            return services;
        }
    }
}
