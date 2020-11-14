using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceStatus
{
    /// <summary>
    /// Represents the <see cref="GetRaceStatusQuery"/> handler.
    /// </summary>
    internal sealed class GetRaceStatusQueryHandler : IQueryHandler<GetRaceStatusQuery, Maybe<RaceStatusResponse>>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetRaceStatusQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Maybe<RaceStatusResponse>> Handle(GetRaceStatusQuery request, CancellationToken cancellationToken)
        {
            if (request.RaceId <= 0)
            {
                return Maybe<RaceStatusResponse>.None;
            }

            Maybe<Race> maybeRace = await _dbContext
                .Set<Race>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.RaceId, cancellationToken);

            if (maybeRace.HasNoValue)
            {
                return Maybe<RaceStatusResponse>.None;
            }

            Race race = maybeRace.Value;

            // TODO: Figure out how to improve performance here.
            var response = new RaceStatusResponse
            {
                RaceId = race.Id,
                StartTimeUtc = race.StartTimeUtc,
                FinishTimeUtc = race.FinishTimeUtc,
                Status = race.Status.ToString(),
                VehiclesByStatuses = await (
                    from vehicle in _dbContext.Set<Vehicle>().AsNoTracking()
                    where vehicle.RaceId == race.Id
                    group vehicle by vehicle.Status
                    into groupedByType
                    select new RaceStatusResponse.VehiclesByStatus
                    {
                        Status = groupedByType.Key.ToString(),
                        Count = groupedByType.Count()
                    }).ToListAsync(cancellationToken),
                VehiclesByTypes = await (
                    from vehicle in _dbContext.Set<Vehicle>().AsNoTracking()
                    where vehicle.RaceId == race.Id
                    group vehicle by vehicle.VehicleType
                    into groupedByType
                    select new RaceStatusResponse.VehiclesByType
                    {
                        Type = groupedByType.Key.ToString(),
                        Count = groupedByType.Count()
                    }).ToListAsync(cancellationToken)
            };

            return response;
        }
    }
}
