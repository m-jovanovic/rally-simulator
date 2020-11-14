using RallySimulator.Domain.Primitives;

namespace RallySimulator.Application.Validation
{
    /// <summary>
    /// Contains the application layer validation errors.
    /// </summary>
    internal static partial class ValidationErrors
    {
        /// <summary>
        /// Contains the vehicle type validation errors.
        /// </summary>
        internal static class VehicleType
        {
            /// <summary>
            /// Gets the vehicle type not valid error.
            /// </summary>
            internal static Error NotValid => new Error("VehicleType.NotValid", "The specified vehicle type is not valid.");
        }
    }
}
