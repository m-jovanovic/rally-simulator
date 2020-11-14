using System.Collections.Generic;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleSubtypes
{
    /// <summary>
    /// Represents the query for getting the vehicle subtypes.
    /// </summary>
    public sealed class GetVehicleSubtypesQuery : IQuery<IReadOnlyCollection<VehicleSubtypeResponse>>
    {
    }
}
