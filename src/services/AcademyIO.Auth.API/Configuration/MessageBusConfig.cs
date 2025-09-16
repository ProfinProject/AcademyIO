using System.Reflection;
using AcademyIO.MessageBus;

namespace AcademyIO.Auth.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration, Assembly.GetAssembly(typeof(Program)));
        }
    }
}
