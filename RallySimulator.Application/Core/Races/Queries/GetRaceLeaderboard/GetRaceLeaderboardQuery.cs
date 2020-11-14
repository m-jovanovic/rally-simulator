using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceLeaderboard
{
    /// <summary>
    /// Represents the query for getting the race leaderboard.
    /// </summary>
    public sealed class GetRaceLeaderboardQuery : IQuery<Maybe<RaceLeaderboardResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceLeaderboardQuery"/> class.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        public GetRaceLeaderboardQuery(int raceId) => RaceId = raceId;

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get; }
    }
}
