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

            /// <summary>
            /// Gets the another race is already running error.
            /// </summary>
            public static Error AnotherRaceIsAlreadyRunning => new Error(
                "Race.AnotherRaceIsAlreadyRunning",
                "There is already a race that is running and the current race can't be started.");
        }
    }
}
