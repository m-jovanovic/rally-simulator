using System;
using RallySimulator.Domain.Primitives.Events;

namespace RallySimulator.Domain.Core.Events
{
    /// <summary>
    /// Represents the event that is raised when a race has started.
    /// </summary>
    public sealed class RaceStartedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RaceStartedDomainEvent"/> class.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <param name="startTimeUtc">The race start time in UTC format.</param>
        internal RaceStartedDomainEvent(int raceId, DateTime startTimeUtc)
        {
            RaceId = raceId;
            StartTimeUtc = startTimeUtc;
        }

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get; }

        /// <summary>
        /// Gets the race start time in UTC format.
        /// </summary>
        public DateTime StartTimeUtc { get; }
    }
}
