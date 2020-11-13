using System;

namespace RallySimulator.Application.Contracts.Vehicles
{
    /// <summary>
    /// Represents the create vehicle request.
    /// </summary>
    public sealed class CreateVehicleRequest
    {
        /// <summary>
        /// Gets or sets the race identifier.
        /// </summary>
        public int RaceId { get; set; }

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
