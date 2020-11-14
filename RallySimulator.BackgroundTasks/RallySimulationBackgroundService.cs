using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RallySimulator.Application.Abstractions.Common;
using RallySimulator.BackgroundTasks.Settings;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;
using RallySimulator.Persistence;

namespace RallySimulator.BackgroundTasks
{
    /// <summary>
    /// Represents the rally simulation background service.
    /// </summary>
    internal sealed class RallySimulationBackgroundService : BackgroundService
    {
        private readonly ILogger<RallySimulationBackgroundService> _logger;
        private readonly BackgroundTaskSettings _backgroundTaskSettings;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDateTime _dateTime;
        private readonly Random _random;

        /// <summary>
        /// Initializes a new instance of the <see cref="RallySimulationBackgroundService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="backgroundTaskSettingsOptions">The background task settings options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dateTime">The date and time.</param>
        public RallySimulationBackgroundService(
            ILogger<RallySimulationBackgroundService> logger,
            IOptions<BackgroundTaskSettings> backgroundTaskSettingsOptions,
            IServiceProvider serviceProvider,
            IDateTime dateTime)
        {
            _logger = logger;
            _backgroundTaskSettings = backgroundTaskSettingsOptions.Value;
            _serviceProvider = serviceProvider;
            _dateTime = dateTime;
            _random = new Random();
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Rally simulation background service is starting.");

            await Task.Delay(5000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Rally simulation background service is doing background work.");

                using IServiceScope scope = _serviceProvider.CreateScope();

                RallySimulatorDbContext dbContext = scope.ServiceProvider.GetRequiredService<RallySimulatorDbContext>();

                Maybe<Race> maybeRace = await dbContext.Set<Race>()
                    .SingleOrDefaultAsync(x => x.Status == RaceStatus.Running, stoppingToken);

                if (maybeRace.HasValue)
                {
                    await SimulateOneHourPassing(maybeRace.Value, dbContext);
                }

                await Task.Delay(_backgroundTaskSettings.SleepTimeInMilliseconds, stoppingToken);
            }

            _logger.LogInformation("Rally simulation background service is stopping.");

            await Task.CompletedTask;
        }

        /// <summary>
        /// Simulates the passing of one hour for the specified running race.
        /// </summary>
        /// <param name="race">The race that is currently running.</param>
        /// <param name="dbContext">The database context.</param>
        private async Task SimulateOneHourPassing(Race race, RallySimulatorDbContext dbContext)
        {
            List<Vehicle> vehicles = await dbContext.Set<Vehicle>()
               .Include(x => x.RepairmentLength)
               .Include(x => x.Speed)
               .Include(x => x.MalfunctionProbability)
               .Include(x => x.Malfunctions)
               .Where(x => x.RaceId == race.Id)
               .ToListAsync();

            VehicleStatus[] brokenOrCompletedRace = { VehicleStatus.Broken, VehicleStatus.CompletedRace };

            foreach (Vehicle vehicle in vehicles.Where(x => !brokenOrCompletedRace.Contains(x.Status)))
            {
                vehicle.SimulateOneHourPassing();

                if (vehicle.Status == VehicleStatus.WaitingForRepair && !vehicle.TryRepair())
                {
                    continue;
                }

                decimal malfunctionProbability = (decimal)_random.NextDouble();

                if (vehicle.MalfunctionProbability.LightMalfunctionProbability >= malfunctionProbability)
                {
                    vehicle.AddLightMalfunction();

                    continue;
                }

                if (vehicle.MalfunctionProbability.HeavyMalfunctionProbability >= malfunctionProbability)
                {
                    vehicle.AddHeavyMalfunction();

                    continue;
                }

                vehicle.IncrementDistance(vehicle.Speed.SpeedInKilometersPerHour * 1.0m, race.LengthInKilometers);

                if (vehicle.DistanceCovered == race.LengthInKilometers)
                {
                    vehicle.CompleteRace();
                }
            }

            race.SimulateOneHourPassing();

            if (vehicles.All(x => brokenOrCompletedRace.Contains(x.Status)))
            {
                race.CompleteRace();
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
