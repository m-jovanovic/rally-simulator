using System;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the race entity.
    /// </summary>
    public sealed class Race : Entity, IAuditableEntity
    {
        /// <summary>
        /// The default race length.
        /// </summary>
        public const int DefaultLength = 10_000;

        /// <summary>
        /// Initializes a new instance of the <see cref="Race"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="lengthInKilometers">The length in kilometers.</param>
        public Race(int id, LengthInKilometers lengthInKilometers)
            : base(id)
        {
            Ensure.NotNull(lengthInKilometers, "The race length in kilometers is required.", nameof(lengthInKilometers));

            LengthInKilometers = lengthInKilometers;
        }

        /// <summary>
        /// Gets the race length in kilometers.
        /// </summary>
        public LengthInKilometers LengthInKilometers { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }
    }
}
