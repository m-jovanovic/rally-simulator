using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the vehicle malfunction probabilities for a specific vehicle subtype.
    /// </summary>
    public sealed class VehicleSubtypeMalfunctionProbability : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleSubtypeMalfunctionProbability"/> class.
        /// </summary>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        /// <param name="lightMalfunctionProbability">The light malfunction probability.</param>
        /// <param name="heavyMalfunctionProbability">The heavy malfunction probability.</param>
        public VehicleSubtypeMalfunctionProbability(
            VehicleSubtype vehicleSubtype,
            MalfunctionProbability lightMalfunctionProbability,
            MalfunctionProbability heavyMalfunctionProbability)
            : base((int)vehicleSubtype)
        {
            Ensure.NotNull(
                lightMalfunctionProbability,
                "The light malfunction probability is required.",
                nameof(lightMalfunctionProbability));
            Ensure.NotNull(
                heavyMalfunctionProbability,
                "The heavy malfunction probability is required.",
                nameof(heavyMalfunctionProbability));

            LightMalfunctionProbability = lightMalfunctionProbability;
            HeavyMalfunctionProbability = heavyMalfunctionProbability;
        }

        /// <summary>
        /// Gets the vehicle subtype.
        /// </summary>
        public VehicleSubtype VehicleSubtype => (VehicleSubtype)Id;

        /// <summary>
        /// Gets the light malfunction probability.
        /// </summary>
        public MalfunctionProbability LightMalfunctionProbability { get; private set; }

        /// <summary>
        /// Gets the heavy malfunction probability.
        /// </summary>
        public MalfunctionProbability HeavyMalfunctionProbability { get; private set; }
    }
}
