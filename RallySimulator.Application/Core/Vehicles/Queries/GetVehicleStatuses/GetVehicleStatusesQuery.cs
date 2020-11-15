using System.Collections.Generic;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleStatuses
{
    /// <summary>
    /// Represents the query for getting the vehicle statuses.
    /// </summary>
    public sealed class GetVehicleStatusesQuery : IQuery<IReadOnlyCollection<VehicleStatusResponse>>
    {
    }
}
