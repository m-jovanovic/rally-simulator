using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleStatistics
{
    /// <summary>
    /// Represents the <see cref="GetVehicleStatisticsQuery"/> handler.
    /// </summary>
    internal sealed class GetVehicleStatisticsQueryHandler : IQueryHandler<GetVehicleStatisticsQuery, Maybe<VehicleStatisticsResponse>>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleStatisticsQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetVehicleStatisticsQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Maybe<VehicleStatisticsResponse>> Handle(GetVehicleStatisticsQuery request, CancellationToken cancellationToken)
        {
            if (request.VehicleId <= 0)
            {
                return Maybe<VehicleStatisticsResponse>.None;
            }

            Maybe<Vehicle> maybeVehicle = await _dbContext.Set<Vehicle>()
                .Include(x => x.RepairmentLength)
                .Include(x => x.Malfunctions)
                .FirstOrDefaultAsync(x => x.Id == request.VehicleId, cancellationToken);

            if (maybeVehicle.HasNoValue)
            {
                return Maybe<VehicleStatisticsResponse>.None;
            }

            Vehicle vehicle = maybeVehicle.Value;

            int lightMalfunctionsCount = vehicle.Malfunctions.Count(x => x.Type == MalfunctionType.Light);

            var response = new VehicleStatisticsResponse
            {
                VehicleId = vehicle.Id,
                RaceId = vehicle.RaceId,
                Distance = $"{vehicle.DistanceCovered.Value} km",
                StartTimeUtc = vehicle.StartTimeUtc,
                FinishTimeUtc = vehicle.FinishTimeUtc,
                Status = vehicle.Status.ToString(),
                LightMalfunctionsCount = lightMalfunctionsCount,
                HeavyMalfunctionsCount = vehicle.Malfunctions.Count - lightMalfunctionsCount,
                HoursSpentOnRepairing = lightMalfunctionsCount * vehicle.RepairmentLength.RepairmentLengthInHours
            };

            return response;
        }
    }
}
