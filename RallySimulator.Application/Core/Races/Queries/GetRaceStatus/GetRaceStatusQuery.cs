using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceStatus
{
    /// <summary>
    /// Represents the query for getting the race status.
    /// </summary>
    public sealed class GetRaceStatusQuery : IQuery<Maybe<RaceStatusResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceStatusQuery"/> class.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        public GetRaceStatusQuery(int raceId) => RaceId = raceId;

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get; }
    }
}
