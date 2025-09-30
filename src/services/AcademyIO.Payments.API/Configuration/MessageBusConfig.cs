using System.Reflection;
using AcademyIO.MessageBus;
using AcademyIO.Core.Utils;

namespace AcademyIO.Payments.API.Configuration
{
    internal static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}
