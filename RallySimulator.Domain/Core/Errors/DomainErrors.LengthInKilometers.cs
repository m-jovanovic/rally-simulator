using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the length in kilometers errors.
        /// </summary>
        public static class LengthInKilometers
        {
            /// <summary>
            /// Gets the length in kilometers is less than zero error.
            /// </summary>
            public static Error LessThanZero => new Error(
                "LengthInKilometers.LessThanZero",
                "The provided length is less than zero.");
        }
    }
}
