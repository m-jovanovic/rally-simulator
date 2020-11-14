using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceLeaderboardForVehicleType
{
    /// <summary>
    /// Represents the query for getting the race leaderboard by vehicle type.
    /// </summary>
    public sealed class GetRaceLeaderboardForVehicleTypeQuery : IQuery<Maybe<RaceLeaderboardForVehicleTypeResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceLeaderboardForVehicleTypeQuery"/> class.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <param name="vehicleType">The vehicle type.</param>
        public GetRaceLeaderboardForVehicleTypeQuery(int raceId, int vehicleType) => (RaceId, VehicleType) = (raceId, vehicleType);

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get; }

        /// <summary>
        /// Gets the vehicle type.
        /// </summary>
        public int VehicleType { get; }
    }
}
