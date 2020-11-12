using System;
using RallySimulator.Domain.Primitives;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the malfunction that can occur on a vehicle during the race.
    /// </summary>
    public sealed class Malfunction : Entity, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Malfunction"/> class.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <param name="malfunctionType">The malfunction type.</param>
        internal Malfunction(Vehicle vehicle, MalfunctionType malfunctionType)
        {
            VehicleId = vehicle.Id;
            Type = malfunctionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Malfunction"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Malfunction()
        {
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public int VehicleId { get; private set; }

        /// <summary>
        /// Gets the malfunction type.
        /// </summary>
        public MalfunctionType Type { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }
    }
}
