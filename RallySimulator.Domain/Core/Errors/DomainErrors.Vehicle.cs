using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the vehicle errors.
        /// </summary>
        public static class Vehicle
        {
            /// <summary>
            /// Gets the vehicle race has started error.
            /// </summary>
            public static Error RaceHasStarted => new Error(
                "Vehicle.RaceHasStarted",
                "The race has started and the vehicle information can't be modified.");

            /// <summary>
            /// Gets the vehicle race has not started error.
            /// </summary>
            public static Error RaceHasNotStarted => new Error(
                "Vehicle.RaceHasNotStarted",
                "The race has not yet started and the vehicle can't suffer a malfunction.");

            /// <summary>
            /// Gets the vehicle is broken error.
            /// </summary>
            public static Error Broken => new Error("Vehicle.Broken", "The vehicle is broken and is out of the race.");

            /// <summary>
            /// Gets the vehicle is waiting for repair error.
            /// </summary>
            public static Error WaitingForRepair => new Error(
                "Vehicle.WaitingForRepair",
                "The vehicle is waiting for repair and can't be modified.");
        }
    }
}
