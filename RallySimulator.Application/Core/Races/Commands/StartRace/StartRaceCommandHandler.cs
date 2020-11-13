using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Common;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;
using RallySimulator.Domain.Primitives.Result;
using RallySimulator.Domain.Services;

namespace RallySimulator.Application.Core.Races.Commands.StartRace
{
    /// <summary>
    /// Represents the <see cref="StartRaceCommand"/> handler.
    /// </summary>
    internal sealed class StartRaceCommandHandler : ICommandHandler<StartRaceCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IRunningRaceChecker _runningRaceChecker;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartRaceCommandHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="runningRaceChecker">The running race checker.</param>
        /// <param name="dateTime">The date and time.</param>
        public StartRaceCommandHandler(IDbContext dbContext, IRunningRaceChecker runningRaceChecker, IDateTime dateTime)
        {
            _dbContext = dbContext;
            _runningRaceChecker = runningRaceChecker;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(StartRaceCommand request, CancellationToken cancellationToken)
        {
            Maybe<Race> maybeRace = await _dbContext.GetBydIdAsync<Race>(request.RaceId);

            if (maybeRace.HasNoValue)
            {
                return Result.Failure(ValidationErrors.Race.NotFound);
            }

            Result result = await maybeRace.Value.StartRace(_runningRaceChecker, _dateTime.UtcNow);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
