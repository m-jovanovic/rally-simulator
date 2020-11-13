using Microsoft.Extensions.DependencyInjection;
using RallySimulator.Application.Abstractions.Common;
using RallySimulator.Infrastructure.Common;

namespace RallySimulator.Infrastructure
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
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDateTime, MachineDateTime>();

            return services;
        }
    }
}
