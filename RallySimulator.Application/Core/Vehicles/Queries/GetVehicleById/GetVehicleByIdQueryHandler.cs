using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleById
{
    /// <summary>
    /// Represents the <see cref="GetVehicleByIdQuery"/> handler.
    /// </summary>
    internal sealed class GetVehicleByIdQueryHandler : IQueryHandler<GetVehicleByIdQuery, Maybe<VehicleResponse>>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetVehicleByIdQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Maybe<VehicleResponse>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.VehicleId <= 0)
            {
                return Maybe<VehicleResponse>.None;
            }

            Maybe<Vehicle> maybeVehicle =
                await _dbContext
                    .Set<Vehicle>()
                    .Include(x => x.Speed)
                    .FirstOrDefaultAsync(x => x.Id == request.VehicleId, cancellationToken);

            if (maybeVehicle.HasNoValue)
            {
                return Maybe<VehicleResponse>.None;
            }

            Vehicle vehicle = maybeVehicle.Value;

            var response = new VehicleResponse
            {
                VehicleId = vehicle.Id,
                RaceId = vehicle.RaceId,
                TeamName = vehicle.TeamName,
                ModelName = vehicle.ModelName,
                ManufacturingDate = vehicle.ManufacturingDate,
                Distance = $"{vehicle.Distance.Value} km",
                Speed = $"{vehicle.Speed.SpeedInKilometersPerHour.Value} km/h",
                Status = vehicle.Status.ToString(),
                VehicleType = vehicle.VehicleType.ToString(),
                VehicleSubtype = vehicle.VehicleSubtype.ToString()
            };

            return response;
        }
    }
}
