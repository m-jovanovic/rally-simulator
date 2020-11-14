using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicleSubtypes;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleTypes
{
    /// <summary>
    /// Represents the <see cref="GetVehicleSubtypesQuery"/> handler.
    /// </summary>
    internal sealed class GetVehicleTypesQueryHandler : IQueryHandler<GetVehicleTypesQuery, IReadOnlyCollection<VehicleTypeResponse>>
    {
        /// <inheritdoc />
        public Task<IReadOnlyCollection<VehicleTypeResponse>> Handle(GetVehicleTypesQuery request, CancellationToken cancellationToken)
        {
            var vehicleSubtypes = (
                from int value in Enum.GetValues(typeof(VehicleType))
                select new VehicleTypeResponse
                {
                    Id = value,
                    Name = Enum.GetName(typeof(VehicleType), value)
                }).ToList();

            return Task.FromResult((IReadOnlyCollection<VehicleTypeResponse>)vehicleSubtypes);
        }
    }
}
