using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Vehicles.Commands.RemoveVehicle
{
    /// <summary>
    /// Represents the command for removing a vehicle from a race.
    /// </summary>
    public sealed class RemoveVehicleCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveVehicleCommand"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        public RemoveVehicleCommand(int vehicleId) => VehicleId = vehicleId;

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; }
    }
}
