using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the model name errors.
        /// </summary>
        public static class ModelName
        {
            /// <summary>
            /// Gets the model name is null or empty error.
            /// </summary>
            public static Error NullOrEmpty => new Error("ModelName.NullOrEmpty", "The model name is required.");

            /// <summary>
            /// Gets the model name is longer than allowed error.
            /// </summary>
            public static Error LongerThanAllowed => new Error("ModelName.LongerThanAllowed", "The model name is longer than allowed.");
        }
    }
}
