using System;

namespace RallySimulator.Application.Contracts.Races
{
    /// <summary>
    /// Represents the leaderboard vehicle.
    /// </summary>
    public class LeaderboardVehicle
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// Gets or sets the finish time.
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// Gets or sets the subtype.
        /// </summary>
        public string VehicleSubtype { get; set; }
    }
}
