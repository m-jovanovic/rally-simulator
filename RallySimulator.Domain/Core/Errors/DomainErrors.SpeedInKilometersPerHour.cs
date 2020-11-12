using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the speed in kilometers per hour errors.
        /// </summary>
        public static class SpeedInKilometersPerHour
        {
            /// <summary>
            /// Gets the speed in kilometers per hour is less than or equal to zero error.
            /// </summary>
            public static Error LessThanOrEqualToZero => new Error(
                "SpeedInKilometersPerHour.LessThanOrEqualToZero",
                "The provided speed is less than or equal to zero.");
        }
    }
}
