using System.Collections.Generic;

namespace RallySimulator.Application.Contracts.Races
{
    /// <summary>
    /// Represents the race leaderboard for vehicle type response.
    /// </summary>
    public sealed class RaceLeaderboardForVehicleTypeResponse
    {
        /// <summary>
        /// Gets or sets the race identifier.
        /// </summary>
        public int RaceId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle type.
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Gets or sets the leaderboard.
        /// </summary>
        public List<LeaderboardVehicle> Leaderboard { get; set; }
    }
}
