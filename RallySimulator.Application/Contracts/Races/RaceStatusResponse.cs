using System;
using System.Collections.Generic;

namespace RallySimulator.Application.Contracts.Races
{
    /// <summary>
    /// Represents the race status response.
    /// </summary>
    public sealed class RaceStatusResponse
    {
        /// <summary>
        /// Gets or sets the race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Gets or sets the race status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the race start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the race finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the vehicles grouped by vehicle status.
        /// </summary>
        public List<VehiclesByStatus> VehiclesByStatus { get; set; }

        /// <summary>
        /// Gets or sets the vehicles grouped by vehicle type.
        /// </summary>
        public List<VehiclesByType> VehiclesByType { get; set; }
    }
}
