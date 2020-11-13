using System;
using System.Threading.Tasks;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;
using RallySimulator.Domain.Services;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the race entity.
    /// </summary>
    public sealed class Race : Entity, IAuditableEntity
    {
        /// <summary>
        /// The default race length.
        /// </summary>
        public const decimal DefaultLength = 10_000;

        /// <summary>
        /// Initializes a new instance of the <see cref="Race"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="lengthInKilometers">The length in kilometers.</param>
        public Race(int year, LengthInKilometers lengthInKilometers)
        {
            Ensure.NotNull(lengthInKilometers, "The race length in kilometers is required.", nameof(lengthInKilometers));
            Ensure.GreaterThanZero(year, "The year must be greater than zero.", nameof(year));

            Year = year;
            LengthInKilometers = lengthInKilometers;
            Status = RaceStatus.Pending;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Race"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Race()
        {
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Gets the length in kilometers.
        /// </summary>
        public LengthInKilometers LengthInKilometers { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public RaceStatus Status { get; private set; }

        /// <summary>
        /// Gets the start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; private set; }

        /// <summary>
        /// Gets the finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <summary>
        /// Starts the race.
        /// </summary>
        /// <param name="runningRaceChecker">The running race checker.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The success result if the operation was successful, otherwise a failure result with an error.</returns>
        public async Task<Result> StartRace(IRunningRaceChecker runningRaceChecker, DateTime utcNow)
        {
            if (StartTimeUtc.HasValue)
            {
                return Result.Failure(DomainErrors.Race.AlreadyStarted);
            }

            if (await runningRaceChecker.IsAnyRaceRunning())
            {
                return Result.Failure(DomainErrors.Race.AnotherRaceIsAlreadyRunning);
            }

            StartTimeUtc = utcNow;

            Status = RaceStatus.Running;

            return Result.Success();
        }

        /// <summary>
        /// Completes the race.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        public void CompleteRace(DateTime utcNow)
        {
            // TODO: Add validation?
            FinishTimeUtc = utcNow;

            Status = RaceStatus.Finished;
        }
    }
}
