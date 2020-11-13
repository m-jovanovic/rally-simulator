using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Common;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives.Maybe;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Races.Commands.StartRace
{
    /// <summary>
    /// Represents the <see cref="StartRaceCommand"/> handler.
    /// </summary>
    internal sealed class StartRaceCommandHandler : ICommandHandler<StartRaceCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartRaceCommandHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="dateTime">The date and time.</param>
        public StartRaceCommandHandler(IDbContext dbContext, IDateTime dateTime)
        {
            _dbContext = dbContext;
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

            if (await _dbContext.Set<Race>().AnyAsync(x => x.Status == RaceStatus.Running, cancellationToken))
            {
                return Result.Failure(DomainErrors.Race.AnotherRaceIsAlreadyRunning);
            }

            Result result = maybeRace.Value.StartRace(_dateTime.UtcNow);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
