using System;

namespace RallySimulator.Application.Contracts.Vehicles
{
    /// <summary>
    /// Represents the update vehicle request.
    /// </summary>
    public sealed class UpdateVehicleRequest
    {
        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the model name.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; set; }

        /// <summary>
        /// Gets or sets the vehicle subtype.
        /// </summary>
        public int VehicleSubtype { get; set; }
    }
}
