using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Common;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicles
{
    /// <summary>
    /// Represents the <see cref="GetVehiclesQuery"/> handler.
    /// </summary>
    internal sealed class GetVehiclesQueryHandler : IQueryHandler<GetVehiclesQuery, PagedList<VehicleResponse>>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GetVehiclesQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<PagedList<VehicleResponse>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            if (request.RaceId <= 0)
            {
                return PagedList<VehicleResponse>.Empty;
            }

            IQueryable<VehicleResponse> vehiclesQuery = _dbContext.Set<Vehicle>()
                .Include(x => x.Speed)
                .AsNoTracking()
                .Where(vehicle =>
                    vehicle.RaceId == request.RaceId &&
                    (string.IsNullOrEmpty(request.Team) || vehicle.TeamName.Value.ToLower().Contains(request.Team)) &&
                    (string.IsNullOrEmpty(request.Model) || vehicle.ModelName.Value.ToLower().Contains(request.Model)) &&
                    (request.ManufacturingDateFrom == null || vehicle.ManufacturingDate >= request.ManufacturingDateFrom) &&
                    (request.ManufacturingDateTo == null || vehicle.ManufacturingDate <= request.ManufacturingDateTo) &&
                    (!request.FilterStatus || vehicle.Status == (VehicleStatus)request.Status) &&
                    (request.DistanceFrom == null || vehicle.Distance.Value >= request.DistanceFrom) &&
                    (request.DistanceTo == null || vehicle.Distance.Value <= request.DistanceTo))
                .OrderBy(request.OrderBy)
                .Select(vehicle => new VehicleResponse
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
                });

            List<VehicleResponse> vehicles = await vehiclesQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            int totalCount = await vehiclesQuery.CountAsync(cancellationToken);

            var response = new PagedList<VehicleResponse>(vehicles, totalCount, request.Page, request.PageSize);

            return response;
        }
    }
}
