using RallySimulator.Domain.Primitives;

namespace RallySimulator.Application.Validation
{
    /// <summary>
    /// Contains the application layer validation errors.
    /// </summary>
    internal static partial class ValidationErrors
    {
        /// <summary>
        /// Contains the race validation errors.
        /// </summary>
        internal static class Race
        {
            /// <summary>
            /// Gets the race year in the past error.
            /// </summary>
            internal static Error YearInThePast => new Error("Race.YearInThePast", "The race year is in the past and is not allowed.");

            /// <summary>
            /// Gets the race length is negative error.
            /// </summary>
            internal static Error NegativeLength => new Error("Race.NegativeLength", "The length is negative.");

            /// <summary>
            /// Gets the race identifier is required error.
            /// </summary>
            internal static Error IdentifierIsRequired => new Error("Race.IdentifierIsRequired", "The race identifier is required.");

            /// <summary>
            /// Gets the race not found error.
            /// </summary>
            internal static Error NotFound => new Error("Race.NotFound", "The race with the specified identifier was not found.");
        }
    }
}
