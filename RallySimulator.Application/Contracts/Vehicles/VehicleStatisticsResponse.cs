using System;

namespace RallySimulator.Application.Contracts.Vehicles
{
    /// <summary>
    /// Represents the vehicle statistics response.
    /// </summary>
    public sealed class VehicleStatisticsResponse
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
        /// Gets or sets the distance.
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the start in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the light malfunctions count.
        /// </summary>
        public int LightMalfunctionsCount { get; set; }

        /// <summary>
        /// Gets or sets the heavy malfunctions count.
        /// </summary>
        public int HeavyMalfunctionsCount { get; set; }

        /// <summary>
        /// Gets or sets the number of hours spent on repairing.
        /// </summary>
        public int HoursSpentOnRepairing { get; set; }
    }
}
