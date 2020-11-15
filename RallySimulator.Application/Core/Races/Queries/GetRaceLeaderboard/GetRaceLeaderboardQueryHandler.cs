using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceLeaderboard
{
    /// <summary>
    /// Represents the <see cref="GetRaceLeaderboardQuery"/> handler.
    /// </summary>
    internal sealed class GetRaceLeaderboardQueryHandler : IQueryHandler<GetRaceLeaderboardQuery, Maybe<RaceLeaderboardResponse>>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceLeaderboardQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetRaceLeaderboardQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Maybe<RaceLeaderboardResponse>> Handle(GetRaceLeaderboardQuery request, CancellationToken cancellationToken)
        {
            if (request.RaceId <= 0)
            {
                return Maybe<RaceLeaderboardResponse>.None;
            }

            List<LeaderboardVehicle> leaderboardVehicles =
                await _dbContext
                    .Set<Vehicle>()
                    .AsNoTracking()
                    .Where(x => x.RaceId == request.RaceId)
                    .OrderBy(x => x.FinishTimeUtc.HasValue)
                    .ThenBy(x => x.FinishTimeUtc)
                    .ThenByDescending(x => x.Distance.Value)
                    .Take(10)
                    .Select(vehicle => new LeaderboardVehicle
                    {
                        VehicleId = vehicle.Id,
                        Distance = $"{vehicle.Distance.Value} km",
                        FinishTime = vehicle.FinishTimeUtc,
                        VehicleSubtype = vehicle.VehicleSubtype.ToString()
                    })
                    .ToListAsync(cancellationToken);

            if (!leaderboardVehicles.Any())
            {
                return Maybe<RaceLeaderboardResponse>.None;
            }

            var response = new RaceLeaderboardResponse
            {
                RaceId = request.RaceId,
                Leaderboard = leaderboardVehicles.Select((x, index) => new LeaderboardVehicle
                {
                    Position = index + 1,
                    VehicleId = x.VehicleId,
                    Distance = x.Distance,
                    FinishTime = x.FinishTime,
                    VehicleSubtype = x.VehicleSubtype
                }).ToList()
            };

            return response;
        }
    }
}
