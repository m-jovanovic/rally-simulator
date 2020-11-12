using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the malfunction probability per hour errors.
        /// </summary>
        public static class MalfunctionProbability
        {
            /// <summary>
            /// Gets the malfunction probability is invalid error.
            /// </summary>
            public static Error InvalidProbability => new Error(
                "MalfunctionProbability.InvalidProbability",
                "The provided probability is not between 0 and 1.");
        }
    }
}
