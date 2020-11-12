using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the vehicle repairment length in hours for a specific vehicle type.
    /// </summary>
    public sealed class VehicleTypeRepairmentLength : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTypeRepairmentLength"/> class.
        /// </summary>
        /// <param name="vehicleType">The vehicle type.</param>
        /// <param name="repairmentLengthInHours">The repairment length in hours.</param>
        public VehicleTypeRepairmentLength(VehicleType vehicleType, int repairmentLengthInHours)
            : base((int)vehicleType)
        {
            Ensure.GreaterThanOrEqualToZero(
                repairmentLengthInHours,
                "The repairment length must be greater than or equal to zero.",
                nameof(repairmentLengthInHours));

            RepairmentLengthInHours = repairmentLengthInHours;
        }

        /// <summary>
        /// Gets the vehicle type.
        /// </summary>
        public VehicleType VehicleType => (VehicleType)Id;

        /// <summary>
        /// Gets the repairment length in hours.
        /// </summary>
        public int RepairmentLengthInHours { get; private set; }
    }
}
