using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the team name errors.
        /// </summary>
        public static class TeamName
        {
            /// <summary>
            /// Gets the team name is null or empty error.
            /// </summary>
            public static Error NullOrEmpty => new Error("TeamName.NullOrEmpty", "The team name is required.");

            /// <summary>
            /// Gets the team name is longer than allowed error.
            /// </summary>
            public static Error LongerThanAllowed => new Error("TeamName.LongerThanAllowed", "The team name is longer than allowed.");
        }
    }
}
