using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the vehicle speed in kilometers per hour for a specific vehicle subtype.
    /// </summary>
    public sealed class VehicleSubtypeSpeed : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleSubtypeSpeed"/> class.
        /// </summary>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        /// <param name="speedInKilometersPerHour">The speed.</param>
        public VehicleSubtypeSpeed(VehicleSubtype vehicleSubtype, SpeedInKilometersPerHour speedInKilometersPerHour)
            : base((int)vehicleSubtype)
        {
            Ensure.NotNull(speedInKilometersPerHour, "The speed is required.", nameof(speedInKilometersPerHour));

            SpeedInKilometersPerHour = speedInKilometersPerHour;
        }

        /// <summary>
        /// Gets the vehicle subtype.
        /// </summary>
        public VehicleSubtype VehicleSubtype => (VehicleSubtype)Id;

        /// <summary>
        /// Gets the speed in kilometers per hour.
        /// </summary>
        public SpeedInKilometersPerHour SpeedInKilometersPerHour { get; private set; }
    }
}
