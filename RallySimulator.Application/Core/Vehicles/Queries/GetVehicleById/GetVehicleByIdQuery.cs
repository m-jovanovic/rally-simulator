using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicleById
{
    /// <summary>
    /// Represents the query for getting the vehicle by identifier.
    /// </summary>
    public sealed class GetVehicleByIdQuery : IQuery<Maybe<VehicleResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleByIdQuery"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        public GetVehicleByIdQuery(int vehicleId) => VehicleId = vehicleId;

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; }
    }
}
