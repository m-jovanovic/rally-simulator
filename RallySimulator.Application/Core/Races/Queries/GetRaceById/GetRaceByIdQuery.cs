using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Domain.Primitives.Maybe;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceById
{
    /// <summary>
    /// Represents the query for getting the race by identifier.
    /// </summary>
    public sealed class GetRaceByIdQuery : IQuery<Maybe<RaceResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceByIdQuery"/> class.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        public GetRaceByIdQuery(int raceId) => RaceId = raceId;

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public int RaceId { get; }
    }
}
