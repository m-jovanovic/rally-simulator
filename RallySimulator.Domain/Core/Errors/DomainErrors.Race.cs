using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the race errors.
        /// </summary>
        public static class Race
        {
            /// <summary>
            /// Gets the race has already started error.
            /// </summary>
            public static Error AlreadyStarted => new Error("Race.AlreadyStarted", "The race has already started.");
        }
    }
}
