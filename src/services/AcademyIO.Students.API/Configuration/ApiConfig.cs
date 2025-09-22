using AcademyIO.ManagementStudents.Data;
using AcademyIO.WebAPI.Core.Configuration;
using AcademyIO.WebAPI.Core.DatabaseFlavor;
using AcademyIO.WebAPI.Core.Identity;
using AcademyIO.MessageBus;
using AcademyIO.Core.Utils;
using static AcademyIO.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;
using NSE.Clientes.API.Services;

namespace AcademyIO.Customers.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureProviderForContext<StudentsContext>(DetectDatabase(configuration));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            services.AddDefaultHealthCheck(configuration);
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

            app.UseAuthConfiguration();

            app.UseDefaultHealthcheck();

            app.MapControllers();

        }

        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                    .AddHostedService<RegistroEstudanteIntegrationHandler>();
        }
    }
}