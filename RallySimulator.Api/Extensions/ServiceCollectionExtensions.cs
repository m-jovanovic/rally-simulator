using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RallySimulator.Api.Extensions
{
    /// <summary>
    /// Contains extensions methods for the service collection class.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the Swagger services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The same service collection.</returns>
        internal static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerGenOptions =>
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rally Simulator API",
                    Version = "v1"
                }));

            return services;
        }
    }
}
