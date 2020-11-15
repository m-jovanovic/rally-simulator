using System;

namespace RallySimulator.Application.Contracts.Races
{
    /// <summary>
    /// Represents the race response.
    /// </summary>
    public sealed class RaceResponse
    {
        /// <summary>
        /// Gets or sets the race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; set; }
    }
}
