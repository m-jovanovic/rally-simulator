using System;
using System.Collections.Generic;
using RallySimulator.Domain.Core;

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
        public List<VehiclesByStatus> VehiclesByStatuses { get; set; }

        /// <summary>
        /// Gets or sets the vehicles grouped by vehicle type.
        /// </summary>
        public List<VehiclesByType> VehiclesByTypes { get; set; }

        /// <summary>
        /// Represents the number of vehicles by vehicle status.
        /// </summary>
        public class VehiclesByStatus
        {
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            public string Status { get; set; }

            /// <summary>
            /// Gets or sets the number of vehicles.
            /// </summary>
            public int Count { get; set; }
        }

        /// <summary>
        /// Represents the number of vehicles by vehicle type.
        /// </summary>
        public class VehiclesByType
        {
            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the number of vehicles.
            /// </summary>
            public int Count { get; set; }
        }
    }
}
