using AcademyIO.WebAPI.Core.Configuration;
using AcademyIO.WebAPI.Core.User;

namespace AcademyIO.Auth.API.Configuration
{

    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddDefaultHealthCheck(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Under certain scenarios, e.g. minikube / linux environment / behind load balancer
            // https redirection could lead dev's to overcomplicated configuration for testing purposes
            // In production is a good practice to keep it true
            if (app.Configuration["USE_HTTPS_REDIRECTION"] == "true")
                app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            //TODO ???
            //app.UseAuthConfiguration();
            app.UseAuthorization();
            app.MapControllers();


            app.UseDefaultHealthcheck();

            return app;
        }
    }
}