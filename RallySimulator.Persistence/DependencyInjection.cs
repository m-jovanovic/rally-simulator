using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RallySimulator.Application.Abstractions.Data;

namespace RallySimulator.Persistence
{
    /// <summary>
    /// Contains the extensions method for registering dependencies in the DI framework.
    /// </summary>
    public static class DependencyInjection
    {
        private static SqliteConnection _sqliteConnection;

        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            _sqliteConnection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));

            if (_sqliteConnection.State != ConnectionState.Open)
            {
                _sqliteConnection.Open();
            }

            services.AddDbContext<RallySimulatorDbContext>(options => options.UseSqlite(_sqliteConnection));

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<RallySimulatorDbContext>());

            return services;
        }
    }
}
