using RallySimulator.Domain.Primitives;

namespace RallySimulator.Application.Validation
{
    /// <summary>
    /// Contains the application layer validation errors.
    /// </summary>
    internal static partial class ValidationErrors
    {
        /// <summary>
        /// Contains the vehicle validation errors.
        /// </summary>
        internal static class Vehicle
        {
            /// <summary>
            /// Gets the vehicle team name is required error.
            /// </summary>
            internal static Error TeamNameIsRequired => new Error("Vehicle.TeamNameIsRequired", "The vehicle team name is required.");

            /// <summary>
            /// Gets the vehicle model name is required error.
            /// </summary>
            internal static Error ModelNameIsRequired => new Error("Vehicle.ModelNameIsRequired", "The vehicle model name is required.");

            /// <summary>
            /// Gets the vehicle manufacturing date is required error.
            /// </summary>
            internal static Error ManufacturingDateIsRequired => new Error(
                "Vehicle.ManufacturingDateIsRequired",
                "The vehicle manufacturing date is required.");

            /// <summary>
            /// Gets the vehicle subtype is required error.
            /// </summary>
            internal static Error SubtypeIsRequired => new Error(
                "Vehicle.SubtypeIsRequired",
                "The vehicle type is required.");

            /// <summary>
            /// Gets the vehicle identifier is required error.
            /// </summary>
            internal static Error IdentifierIsRequired => new Error("Vehicle.IdentifierIsRequired", "The vehicle identifier is required.");

            /// <summary>
            /// Gets the vehicle not found error.
            /// </summary>
            internal static Error NotFound => new Error("Vehicle.NotFound", "The vehicle with the specified identifier was not found.");
        }
    }
}
