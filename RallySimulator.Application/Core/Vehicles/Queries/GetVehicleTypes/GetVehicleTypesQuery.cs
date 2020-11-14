using System.Collections.Generic;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleTypes
{
    /// <summary>
    /// Represents the query for getting the vehicle types.
    /// </summary>
    public sealed class GetVehicleTypesQuery : IQuery<IReadOnlyCollection<VehicleTypeResponse>>
    {
    }
}
