using System;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Vehicles.Commands.UpdateVehicle
{
    /// <summary>
    /// Represents the command for updating vehicle information.
    /// </summary>
    public sealed class UpdateVehicleCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVehicleCommand"/> class.
        /// </summary>
        /// <param name="vehicleId">The race identifier.</param>
        /// <param name="teamName">The team name.</param>
        /// <param name="modelName">The model name.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        public UpdateVehicleCommand(int vehicleId, string teamName, string modelName, DateTime manufacturingDate, int vehicleSubtype)
        {
            VehicleId = vehicleId;
            TeamName = teamName;
            ModelName = modelName;
            ManufacturingDate = manufacturingDate;
            VehicleSubtype = vehicleSubtype;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; }

        /// <summary>
        /// Gets the team name.
        /// </summary>
        public string TeamName { get; }

        /// <summary>
        /// Gets the model name.
        /// </summary>
        public string ModelName { get; }

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; }

        /// <summary>
        /// Gets the vehicle subtype.
        /// </summary>
        public int VehicleSubtype { get; }
    }
}
