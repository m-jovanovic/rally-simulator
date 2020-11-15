using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicleById;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceById
{
    /// <summary>
    /// Represents the <see cref="GetVehicleByIdQuery"/> handler.
    /// </summary>
    internal sealed class GetRaceByIdQueryHandler : IQueryHandler<GetRaceByIdQuery, Maybe<RaceResponse>>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetRaceByIdQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Maybe<RaceResponse>> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.RaceId <= 0)
            {
                return Maybe<RaceResponse>.None;
            }

            Maybe<Race> maybeRace = await _dbContext.Set<Race>().FirstOrDefaultAsync(x => x.Id == request.RaceId, cancellationToken);

            if (maybeRace.HasNoValue)
            {
                return Maybe<RaceResponse>.None;
            }

            Race race = maybeRace.Value;

            var response = new RaceResponse
            {
                RaceId = race.Id,
                Year = race.Year,
                Length = $"{race.LengthInKilometers.Value} km",
                Status = race.Status.ToString(),
                StartTimeUtc = race.StartTimeUtc,
                FinishTimeUtc = race.FinishTimeUtc
            };

            return response;
        }
    }
}
