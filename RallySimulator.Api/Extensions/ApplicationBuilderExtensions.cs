using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RallySimulator.Api.Middleware;
using RallySimulator.Persistence;

namespace RallySimulator.Api.Extensions
{
    /// <summary>
    /// Contains extension methods for configuring the application builder.
    /// </summary>
    internal static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the custom exception handler middleware.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The same application builder.</returns>
        internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<CustomExceptionHandlerMiddleware>();

        /// <summary>
        /// Configures the Swagger and SwaggerUI middleware.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The same application builder.</returns>
        internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder builder)
        {
            builder.UseSwagger();

            builder.UseSwaggerUI(swaggerUiOptions => swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Rally Simulator API"));

            return builder;
        }

        /// <summary>
        /// Ensures that the in-memory database is created.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The same application builder.</returns>
        internal static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder builder)
        {
            using IServiceScope serviceScope = builder.ApplicationServices.CreateScope();

            using RallySimulatorDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<RallySimulatorDbContext>();

            dbContext.Database.EnsureCreated();

            return builder;
        }
    }
}
