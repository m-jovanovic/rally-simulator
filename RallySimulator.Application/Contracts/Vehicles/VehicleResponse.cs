using System;

namespace RallySimulator.Application.Contracts.Vehicles
{
    /// <summary>
    /// Represents the vehicle response.
    /// </summary>
    public sealed class VehicleResponse
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; set; }

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
        /// Gets or sets the vehicle type.
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Gets or sets the vehicle subtype.
        /// </summary>
        public string VehicleSubtype { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public string Speed { get; set; }
    }
}
