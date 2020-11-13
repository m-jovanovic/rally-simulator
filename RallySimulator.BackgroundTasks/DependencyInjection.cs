using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RallySimulator.BackgroundTasks.Settings;

namespace RallySimulator.BackgroundTasks
{
    /// <summary>
    /// Contains the extensions method for registering dependencies in the DI framework.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BackgroundTaskSettings>(configuration.GetSection(BackgroundTaskSettings.SettingsKey));

            services.AddHostedService<RallySimulationBackgroundService>();

            return services;
        }
    }
}
