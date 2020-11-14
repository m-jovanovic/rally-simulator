using System;
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

namespace RallySimulator.Application.Core.Races.Queries.GetRaceLeaderboardForVehicleType
{
    /// <summary>
    /// Represents the <see cref="GetRaceLeaderboardForVehicleTypeQuery"/> handler.
    /// </summary>
    internal sealed class GetRaceLeaderboardForVehicleTypeQueryHandler
        : IQueryHandler<GetRaceLeaderboardForVehicleTypeQuery, Maybe<RaceLeaderboardForVehicleTypeResponse>>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceLeaderboardForVehicleTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetRaceLeaderboardForVehicleTypeQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Maybe<RaceLeaderboardForVehicleTypeResponse>> Handle(
            GetRaceLeaderboardForVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.RaceId <= 0 || !Enum.IsDefined(typeof(VehicleType), request.VehicleType))
            {
                return Maybe<RaceLeaderboardForVehicleTypeResponse>.None;
            }

            var vehicleType = (VehicleType)request.VehicleType;

            List<LeaderboardVehicle> leaderboardVehicles =
                await _dbContext
                    .Set<Vehicle>()
                    .AsNoTracking()
                    .Where(x => x.RaceId == request.RaceId && x.VehicleType == vehicleType)
                    .OrderBy(x => x.FinishTimeUtc.HasValue)
                    .ThenBy(x => x.FinishTimeUtc)
                    .ThenByDescending(x => x.DistanceCovered.Value)
                    .Take(10)
                    .Select(vehicle => new LeaderboardVehicle
                    {
                        VehicleId = vehicle.Id,
                        Distance = $"{vehicle.DistanceCovered.Value} km",
                        FinishTime = vehicle.FinishTimeUtc,
                        VehicleSubtype = vehicle.VehicleSubtype.ToString()
                    })
                    .ToListAsync(cancellationToken);

            if (!leaderboardVehicles.Any())
            {
                return Maybe<RaceLeaderboardForVehicleTypeResponse>.None;
            }

            var response = new RaceLeaderboardForVehicleTypeResponse
            {
                RaceId = request.RaceId,
                VehicleType = vehicleType.ToString(),
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
