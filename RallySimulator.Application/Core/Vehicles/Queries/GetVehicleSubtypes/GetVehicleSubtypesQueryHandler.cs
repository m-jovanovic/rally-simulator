using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleSubtypes
{
    /// <summary>
    /// Represents the <see cref="GetVehicleSubtypesQuery"/> handler.
    /// </summary>
    internal sealed class GetVehicleSubtypesQueryHandler
        : IQueryHandler<GetVehicleSubtypesQuery, IReadOnlyCollection<VehicleSubtypeResponse>>
    {
        /// <inheritdoc />
        public Task<IReadOnlyCollection<VehicleSubtypeResponse>> Handle(GetVehicleSubtypesQuery request, CancellationToken cancellationToken)
        {
            var vehicleSubtypes = (
                from int value in Enum.GetValues(typeof(VehicleSubtype))
                select new VehicleSubtypeResponse
                {
                    Id = value,
                    Name = Enum.GetName(typeof(VehicleSubtype), value)
                }).ToList();

            return Task.FromResult((IReadOnlyCollection<VehicleSubtypeResponse>)vehicleSubtypes);
        }
    }
}
