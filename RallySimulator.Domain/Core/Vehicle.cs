using System;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the vehicle entity.
    /// </summary>
    public sealed class Vehicle : Entity, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="teamName">The team name.</param>
        /// <param name="modelName">The model name.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        /// <param name="vehicleType">The vehicle type.</param>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        public Vehicle(
            int id,
            TeamName teamName,
            ModelName modelName,
            DateTime manufacturingDate,
            VehicleType vehicleType,
            VehicleSubtype vehicleSubtype)
            : base(id)
        {
            Ensure.NotNull(teamName, "The team name is required.", nameof(teamName));
            Ensure.NotNull(modelName, "The model name is required.", nameof(modelName));
            Ensure.NotEmpty(manufacturingDate, "The manufacturing date is required", nameof(manufacturingDate));

            TeamName = teamName;
            ModelName = modelName;
            ManufacturingDate = manufacturingDate.Date;
            VehicleType = vehicleType;
            VehicleSubtype = vehicleSubtype;
        }

        /// <summary>
        /// Gets the team name.
        /// </summary>
        public TeamName TeamName { get; private set; }

        /// <summary>
        /// Gets the model name.
        /// </summary>
        public ModelName ModelName { get; private set; }

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; private set; }

        /// <summary>
        /// Gets the vehicle type.
        /// </summary>
        public VehicleType VehicleType { get; private set; }

        /// <summary>
        /// Gets the vehicle subtype.
        /// </summary>
        public VehicleSubtype VehicleSubtype { get; private set; }

        /// <summary>
        /// Gets the vehicle speed.
        /// </summary>
        public VehicleSubtypeSpeed Speed { get; private set; }

        /// <summary>
        /// Gets the repairment length.
        /// </summary>
        public VehicleTypeRepairmentLength RepairmentLength { get; private set; }

        /// <summary>
        /// Gets the malfunction probability.
        /// </summary>
        public VehicleSubtypeMalfunctionProbability MalfunctionProbability { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }
    }
}
