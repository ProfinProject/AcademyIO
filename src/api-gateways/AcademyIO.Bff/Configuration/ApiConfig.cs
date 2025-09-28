using AcademyIO.Bff.Extensions;
using AcademyIO.WebAPI.Core.Configuration;

namespace AcademyIO.Bff.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.Configure<AppServicesSettings>(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            //TO DO ???
            //services.AddDefaultHealthCheck(configuration)
            //    .AddUrlGroup(new Uri($"{configuration["CourseUrl"]}/healthz-infra"), "Shopping Cart", tags: new[] { "infra" }, configureHttpMessageHandler: _ => HttpExtensions.ConfigureClientHandler())
            //    .AddUrlGroup(new Uri($"{configuration["StudentUrl"]}/healthz-infra"), "Catalog API", tags: new[] { "infra" }, configureHttpMessageHandler: _ => HttpExtensions.ConfigureClientHandler())
            //    .AddUrlGroup(new Uri($"{configuration["PaymentUrl"]}/healthz-infra"), "Billing API", tags: new[] { "infra" }, configureHttpMessageHandler: _ => HttpExtensions.ConfigureClientHandler());
        }

        public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Under certain scenarios, e.g minikube / linux environment / behind load balancer
            // https redirection could lead dev's to over complicated configuration for testing purpouses
            // In production is a good practice to keep it true
            if (app.Configuration["USE_HTTPS_REDIRECTION"] == "true")
                app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            //app.UseDefaultHealthcheck();
        }
    }
}