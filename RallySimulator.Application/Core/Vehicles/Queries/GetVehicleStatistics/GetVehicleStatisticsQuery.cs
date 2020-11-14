using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleStatistics
{
    /// <summary>
    /// Represents the query for getting the vehicle statistics.
    /// </summary>
    public sealed class GetVehicleStatisticsQuery : IQuery<Maybe<VehicleStatisticsResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleStatisticsQuery"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        public GetVehicleStatisticsQuery(int vehicleId) => VehicleId = vehicleId;

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; }
    }
}
