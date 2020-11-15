using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleStatuses
{
    /// <summary>
    /// Represents the <see cref="GetVehicleStatusesQuery"/> handler.
    /// </summary>
    internal sealed class GetVehicleStatusesQueryHandler : IQueryHandler<GetVehicleStatusesQuery, IReadOnlyCollection<VehicleStatusResponse>>
    {
        /// <inheritdoc />
        public Task<IReadOnlyCollection<VehicleStatusResponse>> Handle(GetVehicleStatusesQuery request, CancellationToken cancellationToken)
        {
            var vehicleStatuses = (
                from int value in Enum.GetValues(typeof(VehicleStatus))
                select new VehicleStatusResponse
                {
                    Id = value,
                    Name = Enum.GetName(typeof(VehicleStatus), value)
                }).ToList();

            return Task.FromResult((IReadOnlyCollection<VehicleStatusResponse>)vehicleStatuses);
        }
    }
}
